public class MoveFactory
{
    private readonly IStrategyMoveGhost _scatter;
    private readonly IStrategyMoveGhost _chase;
    private readonly IStrategyMoveGhost _consumed;
    private readonly IStrategyMoveGhost _still;
    public MoveFactory()
    {
        _scatter = new MoveScatter();
        _chase = new MoveChase();
        _consumed = new MoveConsumed();
        _still = new MoveStill();
    }

    public IStrategyMoveGhost GetStrategy(GhostState state)
    {
        return state switch
        {
            GhostState.Scatter => _scatter,
            GhostState.Chase => _chase,
            GhostState.Consumed => _consumed,
            GhostState.Frightened => _scatter,
            GhostState.Still => _still, // puedes tener otro si deseas
            _ => _scatter
        };
    }
}
