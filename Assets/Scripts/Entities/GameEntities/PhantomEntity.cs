public enum GhostState { Scatter, Chase, Frightened, Consumed, Still, }

public enum GhostName { Red, Pink, Blue, Orange }
public class PhantomEntity
{
    public IPosition Position { get; set; }
    public IPosition Direction { get; set; }
    public NodeEntity TargetNode { get; set; }
    public NodeEntity PreviousNode { get; set; }
    public NodeEntity CurrentNode { get; set; }
    public NodeEntity HomeNode { get; set; }
    public IPosition Size { get; set; }
    public float Speed { get; set; }
    public GhostState State { get; set; }
    public GhostName Name { get; set; }

    public void Die()
    {
        State = GhostState.Consumed;
    }

    public void Revive()
    {
        State = GhostState.Scatter;
        Direction = Direction.Zero().Add(new Position(0, 1));
    }

    public void Deactivate()
    {
        State = GhostState.Still;
        Direction = Direction.Zero();
    }
}
