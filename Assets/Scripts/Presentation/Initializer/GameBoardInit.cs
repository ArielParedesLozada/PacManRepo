using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBoardInit : MonoBehaviour
{
    [Header("Referencias lógicas")]
    public GameTilesManager tilesManager;
    public GameGhostManager ghostManager;
    public NodeEntity[] nodes;

    [Header("Instancia lógica")]
    public GameBoardSubject _gameBoardSubject; 
    public GameBoardSubject GameBoardSubject{ get; private set; }
    public void Initialize(GameTilesManager tiles, GameGhostManager ghosts)
    {
        tilesManager = tiles;
        ghostManager = ghosts;
        _gameBoardSubject = new GameBoardSubject();
        _gameBoardSubject.Level = new LevelSetter().GetLevel();
        _gameBoardSubject.Initialize(tilesManager, ghostManager, nodes);
    }
}