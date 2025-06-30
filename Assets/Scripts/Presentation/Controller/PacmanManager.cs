using UnityEngine;
using Zenject;

public class PacmanManager : IInitializable, ITickable
{
    private readonly PacmanEntity _pacman;
    private readonly MovePacman _movePacman;
    private readonly ISubjectGame _game;
    private readonly IStrategyConsume _consume;
    private readonly Transform _transform;

    public PacmanEntity Pacman => _pacman;

    public PacmanManager(
        PacmanEntity pacman,
        MovePacman movePacman,
        ISubjectGame gameBoard,
        IStrategyConsume consumeStrategy,
        [Inject(Id = "PacManTransform")] Transform pacmanTransform)
    {
        _pacman = pacman;
        _movePacman = movePacman;
        _game = gameBoard;
        _consume = consumeStrategy;
        _transform = pacmanTransform;
    }

    public void Initialize()
    {
        Debug.Log("✅ PacmanManager.Initialize ejecutado (Zenject)");
    }

    public void Tick()
    {
        if (_pacman == null || _pacman.PacManState == PacManState.Dead || _pacman.PacManState == PacManState.Still)
            return;
        _pacman.NextDirection = new Position(0, 0);
        if (Input.GetKey(KeyCode.RightArrow))
        {
            _pacman.NextDirection = new Position(1, 0);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _pacman.NextDirection = new Position(-1, 0);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            _pacman.NextDirection = new Position(0, 1);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            _pacman.NextDirection = new Position(0, -1);
        }
        _movePacman.Move(Time.deltaTime);
        TileEntity currentTile = _game.GetTileAt(_pacman.Position);
        // if (_pacman.CurrentNode != null) Debug.Log($"Estoy en la {_pacman.CurrentNode.DebugName}");
        // if (_pacman.TargetNode != null) Debug.Log($"Me dirijo a la {_pacman.TargetNode.DebugName}");
        if (_consume.Consume(_pacman, currentTile))
        {
            // Debug.Log($"juego {_game.Score}");
        }
        _transform.position = new Vector3(_pacman.Position.X, _pacman.Position.Y, 0);
    }
}