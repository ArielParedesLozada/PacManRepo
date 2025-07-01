public class CollisionFrightened : IStrategyGhostCollision
{
    public void Collide(PhantomEntity phantom, PacmanEntity pacman, ISubjectGame game)
    {
        phantom.Die();
    }
}
