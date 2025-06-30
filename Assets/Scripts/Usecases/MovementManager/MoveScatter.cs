public class MoveScatter : IStrategyMoveGhost
{
    public IPosition ChooseTargetTile(PhantomEntity phantom, IPosition pmPos, IPosition pmDir)
    {
        // Usualmente el HomeNode es la esquina asignada al fantasma
        return phantom.HomeNode?.Position ?? new Position(0, 0);
    }

    public void Move(PhantomEntity ghost, PacmanEntity pacman, float deltaTime)
    {
        // Si aún se está moviendo hacia el siguiente nodo
        if (ghost.TargetNode != null && !IsAtTarget(ghost))
        {
            var step = ghost.Direction.Multiply(ghost.Speed * deltaTime);
            ghost.Position = ghost.Position.Add(step);
            return;
        }

        // Al llegar al TargetNode, establecerlo como CurrentNode
        ghost.CurrentNode = ghost.TargetNode;

        // 1. Calcular el "target tile"
        IPosition targetTile = ChooseTargetTile(ghost, pacman.Position, pacman.Direction);

        // 2. Buscar el mejor vecino
        NodeEntity bestNeighbor = null;
        IPosition bestDirection = null;
        float bestDist = float.MaxValue;

        for (int i = 0; i < ghost.CurrentNode.Neighbors.Length; i++)
        {
            var neighbor = ghost.CurrentNode.Neighbors[i];
            var direction = ghost.CurrentNode.ValidDirections[i];

            if (ghost.Direction != null && direction.Multiply(-1).Equals(ghost.Direction))
                continue;

            float dist = neighbor.Position.Subtract(targetTile).SqrMagnitude();
            if (dist < bestDist)
            {
                bestDist = dist;
                bestNeighbor = neighbor;
                bestDirection = direction;
            }
        }

        if (bestNeighbor != null)
        {
            ghost.TargetNode = bestNeighbor;
            ghost.Direction = bestDirection;
        }
        else
        {
            // No hay movimiento válido: quedarse en el nodo actual
            ghost.Direction = null;
            ghost.TargetNode = null;
        }
    }

    private bool IsAtTarget(PhantomEntity ghost)
    {
        var distance = ghost.Position.Subtract(ghost.TargetNode.Position);
        return distance.SqrMagnitude() < 0.01f; // o un margen pequeño
    }
}