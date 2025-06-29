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

        IPosition nextDirection = new Position(0, 0);

        if (Input.GetKeyDown(KeyCode.RightArrow)) nextDirection = new Position(1, 0);
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) nextDirection = new Position(-1, 0);
        else if (Input.GetKeyDown(KeyCode.UpArrow)) nextDirection = new Position(0, 1);
        else if (Input.GetKeyDown(KeyCode.DownArrow)) nextDirection = new Position(0, -1);

        _pacman.NextDirection = nextDirection;
        _movePacman.Move(Time.deltaTime);
        Debug.Log($"Ya se updatea: {_pacman.Position.ToString()}");

        TileEntity currentTile = _game.GetTileAt(_pacman.Position);
        _consume.Consume(_pacman, currentTile);
        if (currentTile != null)
        {
            Debug.Log($"{currentTile.Position.ToString()}");
        }
        else
        {
            Debug.Log("Skibidi Waza sigma");
        }

        _transform.position = new Vector3(_pacman.Position.X, _pacman.Position.Y, 0);
    }
}