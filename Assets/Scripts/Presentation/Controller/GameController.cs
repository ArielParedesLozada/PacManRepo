using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private GameBoardSubject _gameBoard;

    public void Initialize(GameBoardSubject gameBoard)
    {
        _gameBoard = gameBoard;
    }

    // Ejemplo: acceso a score, nivel, vidas
    public int Score => _gameBoard.Score;
    public int Level => _gameBoard.Level;
    public int Lives => _gameBoard.Lives;

    // Ejemplo: acceso a nodos y tiles lógicos
    public NodeEntity GetNodeAt(IPosition position) => _gameBoard.GetNodeAt(position);
    public TileEntity GetTileAt(IPosition position) => _gameBoard.GetTileAt(position);

    // Ejemplo: notificar cambios a los observers (fantasmas, etc.)
    public void Notify()
    {
        _gameBoard.Notify();
    }

    // Puedes agregar métodos para reiniciar el juego, avanzar de nivel, etc.
    public void ResetGame()
    {
        // Lógica para reiniciar el estado del GameBoardSubject y notificar a los managers
        // ...
        Notify();
    }
}
