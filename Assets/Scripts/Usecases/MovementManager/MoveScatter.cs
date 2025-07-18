public class MoveScatter : IStrategyMoveGhost
{
    public IPosition ChooseTargetTile(PhantomEntity phantom, IPosition pmPos, IPosition pmDir)
    {
        // El objetivo en modo Scatter suele ser la esquina asignada (HomeNode)
        return phantom.HomeNode?.Position ?? new Position(0, 0);
    }

    public void Move(PhantomEntity ghost, PacmanEntity pacman, float deltaTime)
    {
        // Si está en movimiento entre nodos
        if (ghost.TargetNode != null && ghost.PreviousNode != null)
        {
            ghost.Position = ghost.Position.Add(ghost.Direction.Multiply(ghost.Speed * deltaTime));

            // Verificar si llegó o sobrepasó el nodo destino
            if (HasOverShotTarget(ghost))
            {
                ghost.Position = ghost.TargetNode.Position;
                ghost.CurrentNode = ghost.TargetNode;
                ghost.PreviousNode = null;
                ghost.TargetNode = null;
            }
            else
            {
                return; // Sigue en trayecto, no decidir aún
            }
        }

        // Si está en un nodo y puede decidir hacia dónde ir
        if (ghost.CurrentNode != null && ghost.TargetNode == null)
        {
            IPosition targetTile = ChooseTargetTile(ghost, pacman.Position, pacman.Direction);

            NodeEntity bestNeighbor = null;
            IPosition bestDirection = null;
            float bestDist = float.MaxValue;

            for (int i = 0; i < ghost.CurrentNode.Neighbors.Length; i++)
            {
                var neighbor = ghost.CurrentNode.Neighbors[i];
                var direction = ghost.CurrentNode.ValidDirections[i];

                if (neighbor == null || direction == null)
                    continue;

                // Evitar retroceder
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
                ghost.Direction = bestDirection;
                ghost.PreviousNode = ghost.CurrentNode;
                ghost.TargetNode = bestNeighbor;
                ghost.CurrentNode = null;
            }
            else
            {
                // No puede moverse, se queda quieto
                ghost.Direction = null;
            }
        }
    }

    private bool HasOverShotTarget(PhantomEntity ghost)
    {
        if (ghost.PreviousNode == null || ghost.TargetNode == null)
            return false;

        var fromPrevToTarget = ghost.TargetNode.Position.Subtract(ghost.PreviousNode.Position);
        var fromPrevToNow = ghost.Position.Subtract(ghost.PreviousNode.Position);

        return fromPrevToNow.SqrMagnitude() >= fromPrevToTarget.SqrMagnitude() - 0.01f;
    }
}