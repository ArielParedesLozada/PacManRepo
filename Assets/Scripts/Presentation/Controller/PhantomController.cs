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
        _ghost.DebugName = name;
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

    public PhantomEntity ToEntity()
    {
        GhostName nombre = GhostName.Red;
        nombre = name switch
        {
            "Ghost_Blinky" => GhostName.Red,
            "Ghost_Pinky" => GhostName.Pink,
            "Ghost_Inky" => GhostName.Blue,
            _ => GhostName.Red,
        };

        if (_ghost == null)
        {
            // Crear entidad básica con valores por defecto
            var pos = new Position(transform.position.x, transform.position.y);
            _ghost = new PhantomEntity
            {
                Position = pos,
                Size = new Position(1, 1), // o el tamaño que manejes
                Direction = new Position(1, 0),
                State = GhostState.Still,
                Name = nombre, // O puedes hacerlo configurable por inspector
                DebugName = name
            };
        }

        return _ghost;
    }
}
