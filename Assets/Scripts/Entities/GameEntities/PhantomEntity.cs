using System.Collections.Generic;

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
    public string DebugName { get; set; }

    public PhantomEntity(
        IPosition position,
        IPosition size,
        IPosition direction,
        GhostName name,
        NodeEntity current,
        NodeEntity home,
        float speed
    )
    {
        Position = position;
        Size = size;
        Direction = direction;
        Name = name;
        CurrentNode = current;
        TargetNode = current;
        PreviousNode = current;
        HomeNode = home;
        State = GhostState.Chase;
        Speed = speed;
    }

    public void Die()
    {
        State = GhostState.Consumed;
    }

    public void Revive()
    {
        Direction = Direction.Zero().Add(new Position(0, -1));
        State = GhostState.Chase;
    }

    public void Deactivate()
    {
        State = GhostState.Still;
        Direction = Direction.Zero();
    }
    public void Activate()
    {
        State = GhostState.Chase;
    }

    public void Scare()
    {
        State = GhostState.Frightened;
    }
    public string PrintSafe()
    {
        var nullFields = new List<string>();

        if (Position == null) nullFields.Add(nameof(Position));
        if (Direction == null) nullFields.Add(nameof(Direction));
        if (TargetNode == null) nullFields.Add(nameof(TargetNode));
        if (PreviousNode == null) nullFields.Add(nameof(PreviousNode));
        if (CurrentNode == null) nullFields.Add(nameof(CurrentNode));
        if (HomeNode == null) nullFields.Add(nameof(HomeNode));
        if (Size == null) nullFields.Add(nameof(Size));
        if (DebugName == null) nullFields.Add(nameof(DebugName));

        // Los enums y valores primitivos nunca son null, no necesitan comprobación

        return nullFields.Count > 0
            ? $"Campos nulos: {string.Join(", ", nullFields)}"
            : "Todos los campos tienen valores";
    }
}
