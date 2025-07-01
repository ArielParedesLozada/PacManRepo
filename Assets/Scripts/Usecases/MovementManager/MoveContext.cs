public class MoveContext
{
    private IStrategyMoveGhost _strategy;

    public void SetStrategy(IStrategyMoveGhost strategy)
    {
        _strategy = strategy;
    }
    public void Execute(PhantomEntity phantom, PacmanEntity pacman, float deltaTime)
    {
        _strategy.Move(phantom, pacman, deltaTime);
    }
}
