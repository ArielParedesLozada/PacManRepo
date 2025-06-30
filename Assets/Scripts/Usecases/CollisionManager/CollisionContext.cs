public class CollisionContext
{
    private IStrategyGhostCollision _strategy;

    public void SetStrategy(IStrategyGhostCollision strategy)
    {
        _strategy = strategy;
    }
    public void Execute(PhantomEntity phantom, PacmanEntity pacman, ISubjectGame game)
    {
        _strategy.Collide(phantom, pacman, game);
    }
}
