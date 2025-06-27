using System;

public enum PacManState { Alive, Dead, Still, Empowered,}
public class PacmanEntity
{
    public IPosition Size { get; set; } = null;
    public IPosition Position { get; set; }
    public IPosition Direction { get; set; }
    public NodeEntity CurrentNode { get; set; }
    public NodeEntity TargetNode { get; set; }
    public PacManState PacManState { get; set; }
    public float Speed { get; private set; }

    public PacmanEntity(NodeEntity startNode, float startSpeed, IPosition size)
    {
        CurrentNode = startNode;
        Position = startNode.Position;
        Direction = new Position(0, 0);
        Speed = startSpeed;
        Size = size ?? new Position(1, 1);
        PacManState = PacManState.Still;
    }

    public void SetSpeed(int level)
    {
        Speed = Math.Clamp(5f + level, 5f, 10f);
    }
    public void Die()
    {
        PacManState = PacManState.Dead;
        Speed = 0;
        Direction = null;
    }

    public void Empower()
    {
        PacManState = PacManState.Empowered;
    }
}
