public class CollisionFactory
{
    private readonly IStrategyGhostCollision _active;
    private readonly IStrategyGhostCollision _frightened;
    private readonly IStrategyGhostCollision _inactive;

    public CollisionFactory()
    {
        _active = new CollisionActive();
        _frightened = new CollisionFrightened();
        _inactive = new CollisionInactive();
    }

    public IStrategyGhostCollision GetStrategy(GhostState state)
    {
        return state switch
        {
            GhostState.Scatter => _active,
            GhostState.Chase => _active,
            GhostState.Consumed => _inactive,
            GhostState.Frightened => _frightened,
            GhostState.Still => _active, // puedes tener otro si deseas
            _ => _active
        };
    }
}
