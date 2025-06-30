using UnityEngine;

public class MoveFrightened : IStrategyMoveGhost
{
    public IPosition ChooseTargetTile(PhantomEntity phantom, IPosition pmPos, IPosition pmDir)
    {
        return pmPos.Add(pmDir).Multiply(-1);
    }

    public void Move(PhantomEntity ghost, PacmanEntity pacman, float deltaTime)
    {
        Debug.Log("ME MUEVO MODO FRIGHTENED SIGMA");
        // 1. Calcular el "target tile" (posición objetivo) según el tipo de fantasma
        IPosition targetTile = ChooseTargetTile(ghost, pacman.Position, pacman.Direction);

        // 2. Buscar el vecino que más acerque al objetivo (evitando retroceder)
        NodeEntity bestNeighbor = null;
        IPosition bestDirection = null;
        float bestDist = float.MaxValue;

        for (int i = 0; i < ghost.CurrentNode.Neighbors.Length; i++)
        {
            var neighbor = ghost.CurrentNode.Neighbors[i];
            var direction = ghost.CurrentNode.ValidDirections[i];

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

        // 3. Si hay un vecino válido, moverse hacia él
        if (bestNeighbor != null)
        {
            ghost.Direction = bestDirection;
            ghost.CurrentNode = null;
            ghost.Position = ghost.Position.Add(ghost.Direction.Multiply(ghost.Speed * deltaTime));
            ghost.CurrentNode = bestNeighbor;
        }
        else
        {
            // Si no hay movimiento posible, quedarse quieto
            ghost.Direction = null;
        }
    }
}
