public class CollisionActive : IStrategyGhostCollision
{
    public void Collide(PhantomEntity phantom, PacmanEntity pacman, ISubjectGame game)
    {
        phantom.Deactivate();
        pacman.Die();
        game.Notify();
    }
}
