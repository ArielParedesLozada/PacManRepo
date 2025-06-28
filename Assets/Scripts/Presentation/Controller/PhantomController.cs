using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhantomController : MonoBehaviour
{
    private IStrategyMoveGhost _moveGhost;
    private PhantomEntity _ghost;
    private PacmanEntity _pacman;
    private ISubjectGame _gameBoard;

    public void Initialize(
        PhantomEntity ghost,
        IStrategyMoveGhost moveGhost,
        PacmanEntity pacman,
        ISubjectGame gameBoard)
    {
        _ghost = ghost;
        _moveGhost = moveGhost;
        _pacman = pacman;
        _gameBoard = gameBoard;
    }

    void Update()
    {
        if (_ghost == null || _moveGhost == null || _pacman == null || _gameBoard == null)
            return;

        // Solo mover si el fantasma está activo
        if (_ghost.State == GhostState.Still)
            return;

        // Ejecutar la lógica de movimiento del fantasma
        _moveGhost.Move(_ghost, _pacman, Time.deltaTime);

        // Actualizar la posición visual del GameObject
        transform.position = new Vector3(_ghost.Position.X, _ghost.Position.Y, 0);
    }

    public PhantomEntity ToEntity() => _ghost;
}
