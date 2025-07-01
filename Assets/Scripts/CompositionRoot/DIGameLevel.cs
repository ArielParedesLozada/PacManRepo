using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DIGameLevel : MonoInstaller
{
    [Header("Inicializadores")]
    public NodeInit nodeInit;
    public TileInit tileInit;
    public PacmanInit pacmanInit;
    public PhantomInit phantomInit;
    public GameBoardInit gameBoardInit;

    public override void InstallBindings()
    {
        Debug.Log("🛠️ DIGameLevel.InstallBindings iniciado");

        // --- 1. Nodos
        var nodeEntities = nodeInit.Initialize();
        Debug.Log($"📍 NodeEntities inicializados: {nodeEntities.Length}");

        // --- 2. Tiles
        var tilesManager = tileInit.Initialize();
        Debug.Log("✅ TilesManager inicializado");
        Container.Bind<GameTilesManager>().FromInstance(tilesManager).AsSingle();

        // --- 3. Pac-Man
        if (pacmanInit == null)
        {
            Debug.LogError("❌ PacmanInit no está asignado.");
            return;
        }

        pacmanInit.Initialize();
        var pacmanEntity = pacmanInit.pacmanEntity;
        var movePacman = pacmanInit.movePacman;
        if (pacmanEntity == null || movePacman == null)
        {
            Debug.LogError("❌ PacmanEntity o MovePacman no fue creado correctamente.");
            return;
        }

        Debug.Log("✅ PacmanEntity y MovePacman inicializados");

        // Transform de PacMan (usamos el tag "PacMan")
        GameObject pacmanGO = GameObject.FindWithTag("PacMan");
        if (pacmanGO == null)
        {
            Debug.LogError("❌ GameObject con tag 'PacMan' no encontrado.");
            return;
        }
        Transform pacmanTransform = pacmanGO.transform;
        Container.Bind<Transform>().WithId("PacManTransform").FromInstance(pacmanTransform).AsSingle();

        // Bindeos necesarios para PacmanManager
        Container.Bind<PacmanEntity>().FromInstance(pacmanEntity).AsSingle();
        Container.Bind<MovePacman>().FromInstance(movePacman).AsSingle();

        // --- 4. Fantasmas
        var ghostManager = phantomInit.InitializeGhosts(pacmanEntity);
        Debug.Log("👻 GhostManager inicializado");
        Container.Bind<GameGhostManager>().FromInstance(ghostManager).AsSingle();

        // --- 5. GameBoardInit
        gameBoardInit.tilesManager = tilesManager;
        gameBoardInit.ghostManager = ghostManager;

        // Detectar nodos automáticamente si están vacíos
        if (gameBoardInit.nodes == null || gameBoardInit.nodes.Length == 0)
        {
            var nodeControllers = GameObject.FindObjectsOfType<NodeController>();
            var nodesList = new List<NodeEntity>();
            foreach (var nc in nodeControllers)
            {
                if (nc != null)
                    nodesList.Add(nc.ToEntity());
            }
            gameBoardInit.nodes = nodesList.ToArray();
            Debug.Log($"📍 Nodos detectados automáticamente: {gameBoardInit.nodes.Length}");
        }

        // Inicializar GameBoard
        gameBoardInit.Initialize(tilesManager, ghostManager);
        var gameBoardSubject = gameBoardInit._gameBoardSubject;

        if (gameBoardSubject == null)
        {
            Debug.LogError("❌ GameBoardSubject no fue creado correctamente.");
            return;
        }

        Debug.Log("✅ GameBoardSubject inicializado");
        Container.Bind<GameBoardSubject>().FromInstance(gameBoardSubject).AsSingle();
        Container.Bind<ISubjectGame>().FromInstance(gameBoardSubject).AsSingle(); // importante para lógica

        // --- 6. Lógica de consumo
        Container.Bind<IStrategyConsume>().To<ConsumePellet>().AsSingle()
                .WithArguments(Container.Resolve<ISubjectGame>());
        // --- 7. Vincular PacmanManager (lógica pura, sin MonoBehaviour)
        Container.BindInterfacesAndSelfTo<PacmanManager>().AsSingle();

        #region ViewsPacman
        Container.Bind<FacadePacmanView>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayMovement>().FromComponentInHierarchy().AsSingle();
        Container.Bind<PlayDeath>().FromComponentInHierarchy().AsSingle();
        #endregion

        // --- 8. Otros (opcional)
        Container.Bind<PacmanInit>().FromInstance(pacmanInit).AsSingle();
        Container.Bind<PhantomInit>().FromInstance(phantomInit).AsSingle();

        #region Reseter
        Container.Bind<ResetGame>().AsSingle();
        Container.Bind<LoseGame>().AsSingle();
        Container.Bind<FinishGameController>().FromComponentInHierarchy().AsSingle();
        #endregion

        #region GameController
        Container.Bind<GameController>().AsSingle();
        #endregion
        Debug.Log("🏁 DIGameLevel.InstallBindings finalizado correctamente");
    }
}
