using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCollisionUseCase : ICheckCollisionUseCase
{
    public void Execute(PacManEntity pacMan, GhostEntity ghost, IGameBoardGateway gameBoard)
    {
        Rect pacManRect = new Rect(pacMan.Position, pacMan.Size / 4);
        Rect ghostRect = new Rect(ghost.Position, ghost.Size / 4);

        if (ghostRect.Overlaps(pacManRect))
        {
            if (ghost.Mode == GhostMode.Frightened)
            {
                ghost.Consume();
                gameBoard.StartConsumed(ghost.View);
                return;
            }

            if (ghost.Mode != GhostMode.Consumed)
            {
                Debug.Log("Ghost colision� con Pac-Man. Iniciando muerte.");
                gameBoard.StartDeath();
            }
        }
    }
}
