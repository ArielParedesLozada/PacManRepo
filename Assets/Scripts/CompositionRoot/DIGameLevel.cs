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
        var nodeEntities = nodeInit.Initialize();
        Debug.Log("NodeEntities inicializadas");
        // --- 1. Inicializar Tiles
        var tilesManager = tileInit.Initialize();
        Debug.Log("✅ TilesManager inicializado");
        Container.Bind<GameTilesManager>().FromInstance(tilesManager).AsSingle();

        // --- 2. Inicializar Pacman
        if (pacmanInit == null)
        {
            Debug.LogError("❌ PacmanInit no está asignado en DIGameLevel.");
            return;
        }

        pacmanInit.Initialize();
        var pacmanManager = pacmanInit.pacmanManager;
        var pacmanEntity = pacmanManager?.PacMan;

        if (pacmanEntity == null)
        {
            Debug.LogError("❌ PacmanEntity no fue creado correctamente.");
            return;
        }

        Debug.Log("✅ PacmanEntity y PacmanManager inicializados");
        Container.Bind<PacmanEntity>().FromInstance(pacmanEntity).AsSingle();
        Container.Bind<PacmanManager>().FromInstance(pacmanManager).AsSingle();

        // --- 3. Inicializar Fantasmas
        var ghostManager = phantomInit.InitializeGhosts(pacmanEntity);
        Debug.Log("👻 GhostManager inicializado");
        Container.Bind<GameGhostManager>().FromInstance(ghostManager).AsSingle();

        // --- 4. Inicializar GameBoard (y nodos automáticamente si no están)
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

        gameBoardInit.Initialize(tilesManager, ghostManager);

        var gameBoardSubject = gameBoardInit._gameBoardSubject;
        if (gameBoardSubject == null)
        {
            Debug.LogError("❌ GameBoardSubject no fue creado en GameBoardInit.");
            return;
        }

        Debug.Log("✅ GameBoardSubject inicializado");
        Container.Bind<GameBoardSubject>().FromInstance(gameBoardSubject).AsSingle();

        // Vincular PacmanInit si necesitas inyectarlo en GameInit u otros lugares
        Container.Bind<PacmanInit>().FromInstance(pacmanInit).AsSingle();

        Debug.Log("🏁 DIGameLevel.InstallBindings finalizado correctamente");
    }
}
