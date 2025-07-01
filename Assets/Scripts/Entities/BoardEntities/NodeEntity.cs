public class NodeEntity
{
    public NodeEntity[] Neighbors { get; private set; }
    public IPosition[] ValidDirections { get; private set; }
    public IPosition Position { get; }
    public string DebugName { get; set; }

    public NodeEntity(IPosition position, int neighborCount)
    {
        Position = position ?? throw new System.ArgumentNullException(nameof(position));
        Neighbors = new NodeEntity[neighborCount];
        ValidDirections = new IPosition[neighborCount];
    }

    public void SetNeighborsAndDirections(NodeEntity[] neighbors)
    {
        Neighbors = neighbors ?? new NodeEntity[0];
        ValidDirections = new IPosition[Neighbors.Length];

        for (int i = 0; i < Neighbors.Length; i++)
        {
            var neighbor = Neighbors[i];

            if (neighbor == null)
            {
                ValidDirections[i] = null;
                continue;
            }

            if (neighbor.Position == null)
            {
                continue;
            }

            ValidDirections[i] = neighbor.Position.Subtract(Position).Normalize();
        }
    }

    public NodeEntity GetNeighborInDirection(IPosition dir)
    {
        for (int i = 0; i < ValidDirections.Length; i++)
        {
            if (ValidDirections[i] != null && ValidDirections[i].Equals(dir))
                return Neighbors[i];
        }
        return null;
    }
}
