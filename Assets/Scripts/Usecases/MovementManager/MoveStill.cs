public class MoveStill : IStrategyMoveGhost
{
    public IPosition ChooseTargetTile(PhantomEntity phantom, IPosition pmPos, IPosition pmDir)
    {
        return phantom.Position;
    }

    public void Move(PhantomEntity phantom, PacmanEntity pacman, float deltaTime)
    {
    }
}
