using UnityEngine;

public class PhantomController : MonoBehaviour
{
    private MoveFactory _strategyFactory;
    private PhantomEntity _ghost;
    private PacmanEntity _pacman;
    private ISubjectGame _gameBoard;
    [Header("Configuracion")]
    public NodeController _homeNode;
    public NodeController _targetNode;

    public void Initialize(
        PhantomEntity ghost,
        MoveFactory strategyFactory,
        PacmanEntity pacman,
        ISubjectGame gameBoard)
    {
        _ghost = ghost;
        _strategyFactory = strategyFactory;
        _pacman = pacman;
        _gameBoard = gameBoard;
        _ghost.DebugName = name;
    }

    void Update()
    {
        if (_ghost == null || _strategyFactory == null || _pacman == null || _gameBoard == null)
            return;

        if (_ghost.State == GhostState.Still)
            return;

        var moveStrategy = _strategyFactory.GetStrategy(_ghost.State);
        moveStrategy.Move(_ghost, _pacman, Time.deltaTime);
        Debug.Log($"SOY {_ghost.Name} Y MI ESTADO ES {_ghost.State} WAZA");
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
            var speed = new LevelSetter().GetLevel();
            // Crear entidad básica con valores por defecto
            var pos = new Position(transform.position.x, transform.position.y);
            _ghost = new PhantomEntity
            (
                pos,
                new Position(1, 1), // o el tamaño que manejes
                new Position(0, 1),
                nombre,// O puedes hacerlo configurable por inspector
                _homeNode.NodeEntity,
                _targetNode.NodeEntity,
                speed
            );
            _ghost.DebugName = name;
        }

        return _ghost;
    }
}
