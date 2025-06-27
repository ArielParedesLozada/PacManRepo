public class NodeEntity
{
    public NodeEntity[] Neighbors { get; }
    public IPosition[] ValidDirections { get; }

    public IPosition Position { get; }

    public NodeEntity(IPosition position, NodeEntity[] neighbors)
    {
        Position = position;
        Neighbors = neighbors ?? new NodeEntity[0];
        ValidDirections = new IPosition[Neighbors.Length];

        for (int i = 0; i < Neighbors.Length; i++)
        {
            var dir = Neighbors[i].Position.Subtract(Position).Normalize();
            ValidDirections[i] = dir;
        }
    }

    /// <summary>
    /// Devuelve el nodo vecino en la dirección indicada, si existe.
    /// </summary>
    public NodeEntity GetNeighborInDirection(IPosition dir)
    {
        for (int i = 0; i < ValidDirections.Length; i++)
        {
            if (ValidDirections[i].Equals(dir))
                return Neighbors[i];
        }
        return null;
    }
}
