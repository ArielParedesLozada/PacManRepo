using UnityEngine;

public class PhantomController : MonoBehaviour
{
    private MoveFactory _strategyFactory;
    private CollisionFactory _collisionFactory;
    private MoveContext _moveContext;
    private CollisionContext _collisionContext;
    private PhantomEntity _ghost;
    public PhantomEntity Phantom
    {
        get { return _ghost; }
    }
    private PacmanEntity _pacman;
    private ISubjectGame _gameBoard;
    [Header("Configuracion")]
    public NodeController _homeNode;
    public NodeController _targetNode;

    public void Initialize(
        PhantomEntity ghost,
        MoveFactory strategyFactory,
        CollisionFactory collisionFactory,
        MoveContext context,
        CollisionContext collisionContext,
        PacmanEntity pacman,
        ISubjectGame gameBoard)
    {
        _ghost = ghost;
        _strategyFactory = strategyFactory;
        _collisionFactory = collisionFactory;
        _pacman = pacman;
        _moveContext = context;
        _collisionContext = collisionContext;
        _gameBoard = gameBoard;
        _ghost.DebugName = name;
    }

    void Update()
    {
        if (_ghost == null || _strategyFactory == null || _pacman == null || _gameBoard == null)
            return;

        if (_ghost.State == GhostState.Still)
            return;
        // Debug.Log($"DIGANOSTICO PAPU {_ghost.DebugName} : {_ghost.PrintSafe()}");
        var moveStrategy = _strategyFactory.GetStrategy(_ghost.State);
        var collisionStrategy = _collisionFactory.GetStrategy(_ghost.State);
        _moveContext.SetStrategy(moveStrategy);
        _collisionContext.SetStrategy(collisionStrategy);
        // Debug.Log($"TIPO DE MOVIMIENTO {moveStrategy.GetType().Name} SOY {_ghost.DebugName}");
        _moveContext.Execute(_ghost, _pacman, Time.deltaTime);
        if (HasCollidedWithPacman())
        {
            _collisionContext.Execute(_ghost, _pacman, _gameBoard);
        }
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
            var level = new LevelSetter().GetLevel();
            // Crear entidad básica con valores por defecto
            var pos = new Position(transform.position.x, transform.position.y);
            _ghost = new PhantomEntity
            (
                pos,
                new Position(1, 1), // o el tamaño que manejes
                new Position(0, 1),
                nombre,// O puedes hacerlo configurable por inspector
                _homeNode.NodeEntity,
                _targetNode.NodeEntity
            );
            _ghost.SetSpeed(level);
            _ghost.DebugName = name;
        }

        return _ghost;
    }
    public bool HasCollidedWithPacman()
    {
        Rect ghostRect = new Rect(_ghost.Position.X, _ghost.Position.Y, _ghost.Size.X / 2, _ghost.Size.Y / 2);
        Rect pacRect = new Rect(_pacman.Position.X, _pacman.Position.Y, _pacman.Size.X / 2, _pacman.Size.Y / 2);
        return ghostRect.Overlaps(pacRect);
    }
}
