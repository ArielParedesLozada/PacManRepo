using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanManager : MonoBehaviour
{
    private ISubjectGame _game;
    private IStrategyConsume _consume;
    private MovePacman _movePacman;
    private PacmanEntity _pacman;
    public PacmanEntity PacMan
    {
        get
        {
            return _pacman;
        }
    }
    private bool _isReady;

    void Update()
    {
        if (!_isReady || _pacman == null || _pacman.PacManState == PacManState.Dead || _pacman.PacManState == PacManState.Still)
        {
            return;
        }
        IPosition nextDirection = new Position(0, 0);
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nextDirection = new Position(1, 0);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            nextDirection = new Position(-1, 0);

        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            nextDirection = new Position(0, 1);

        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            nextDirection = new Position(0, -1);
        }
        _pacman.NextDirection = nextDirection;
        _movePacman.Move(1f);
        TileEntity currentTile = _game.GetTileAt(_pacman.Position);
        _consume.Consume(_pacman, currentTile);
        Transform();
    }

    private void Transform()
    {
        transform.position = new Vector3(_pacman.Position.X, _pacman.Position.Y, 0);
    }
}
