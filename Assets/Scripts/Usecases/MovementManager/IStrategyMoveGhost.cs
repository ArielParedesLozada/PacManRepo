public interface IStrategyMoveGhost
{
    public void Move(PhantomEntity phantom, PacmanEntity pacman, ISubjectGame game, float deltaTime);
    public IPosition ChooseTargetTile(PhantomEntity phantom, IPosition pmPos, IPosition pmDir);
    public IPosition CanMove(PhantomEntity g, IPosition dir, IPosition targetPosition);
    public bool Overshot(PhantomEntity ghost);
}
