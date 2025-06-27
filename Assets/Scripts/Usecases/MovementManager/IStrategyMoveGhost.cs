public interface IStrategyMoveGhost
{
    public void Move(PhantomEntity phantom, PacmanEntity pacman, float deltaTime);
    public IPosition ChooseTargetTile(PhantomEntity phantom, IPosition pmPos, IPosition pmDir);
}
