public class MoveConsumed : IStrategyMoveGhost
{
    public void Move(PhantomEntity ghost, PacmanEntity pacman, float deltaTime)
    {
        // Si ya está en la casa, cambia a Scatter y detente
        if (ghost.Position.DistanceTo(ghost.HomeNode.Position) < 0.1f)
        {
            ghost.State = GhostState.Scatter;
            ghost.Direction = null;
            ghost.CurrentNode = ghost.HomeNode; // Opcional: asegúrate de que esté en el nodo correcto
            return;
        }

        // Si está en un nodo, elige el vecino más cercano a la casa
        if (ghost.CurrentNode != null)
        {
            NodeEntity bestNeighbor = null;
            IPosition bestDirection = null;
            float bestDist = float.MaxValue;

            for (int i = 0; i < ghost.CurrentNode.Neighbors.Length; i++)
            {
                var neighbor = ghost.CurrentNode.Neighbors[i];
                var direction = ghost.CurrentNode.ValidDirections[i];

                // Evita retroceder
                if (ghost.Direction != null && direction.Multiply(-1).Equals(ghost.Direction))
                    continue;

                float dist = neighbor.Position.DistanceTo(ghost.HomeNode.Position);
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
        }

        // Si está en tránsito hacia un nodo
        if (ghost.TargetNode != null && ghost.CurrentNode != ghost.TargetNode)
        {
            ghost.Position = ghost.Position.Add(ghost.Direction.Multiply(ghost.Speed * deltaTime));

            // Si llegó o sobrepasó el nodo destino
            if (Overshot(ghost))
            {
                ghost.CurrentNode = ghost.TargetNode;
                ghost.Position = ghost.CurrentNode.Position;
                ghost.TargetNode = null;
            }
        }
    }

    public IPosition ChooseTargetTile(PhantomEntity phantom, IPosition pmPos, IPosition pmDir)
    {
        return phantom.HomeNode.Position;
    }

    public IPosition CanMove(PhantomEntity g, IPosition dir, IPosition targetPosition)
    {
        // Implementación opcional si necesitas exponer esta lógica
        return null;
    }

    public bool Overshot(PhantomEntity ghost)
    {
        if (ghost.TargetNode == null || ghost.PreviousNode == null)
            return false;

        var fromPrevToTarget = ghost.TargetNode.Position.Subtract(ghost.PreviousNode.Position);
        var fromPrevToNow = ghost.Position.Subtract(ghost.PreviousNode.Position);

        return fromPrevToNow.SqrMagnitude() >= fromPrevToTarget.SqrMagnitude() - 0.01f;
    }
}
