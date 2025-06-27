public class CollisionFrightened : IStrategyGhostCollision
{
    private PacmanEntity _pacMan;
    public CollisionFrightened(PacmanEntity pacman)
    {
        _pacMan = pacman;
    }
    public void Collide(PhantomEntity phantom)
    {
        phantom.Die();
    }
}
