using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DIGameLevel : MonoInstaller
{
    public TileInit tileInit;
    public PacmanInit pacmanInit;
    public PhantomInit phantomInit;
    public GameBoardInit gameBoardInit;

    public override void InstallBindings()
    {
        // 1. Inicializar Tiles
        var tilesManager = tileInit.Initialize();
        Container.Bind<GameTilesManager>().FromInstance(tilesManager).AsSingle();

        // 2. Inicializar Pacman
        // PacmanInit ya inicializa el PacmanManager y la entidad Pacman
        // Si necesitas la entidad lógica:
        var pacmanManager = pacmanInit.pacmanManager;
        var pacmanEntity = pacmanManager.PacMan;
        Container.Bind<PacmanEntity>().FromInstance(pacmanEntity).AsSingle();
        Container.Bind<PacmanManager>().FromInstance(pacmanManager).AsSingle();

        // 3. Inicializar Fantasmas (requiere PacmanEntity)
        var ghostManager = phantomInit.InitializeGhosts(pacmanEntity);
        Container.Bind<GameGhostManager>().FromInstance(ghostManager).AsSingle();

        // 4. Inicializar GameBoard (requiere Tiles, Ghosts y nodos)
        // Asume que los nodos ya están asignados en el inspector de GameBoardInit
        gameBoardInit.tilesManager = tilesManager;
        gameBoardInit.ghostManager = ghostManager;
        gameBoardInit.Initialize(tilesManager, ghostManager);
        var gameBoardSubject = gameBoardInit._gameBoardSubject;
        Container.Bind<GameBoardSubject>().FromInstance(gameBoardSubject).AsSingle();

        // Ahora puedes inyectar GameBoardSubject, GameTilesManager, GameGhostManager, PacmanEntity, etc. en cualquier otro componente que lo requiera.
    }
}
