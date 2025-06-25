using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumeBonusItemUseCase : IConsumeBonusItemUseCase
{
    private readonly IGameBoardGateway _board;

    public ConsumeBonusItemUseCase(IGameBoardGateway board)
    {
        _board = board;
    }

    public bool Execute(Vector2 pacmanPos, bool isPlayerOne)
    {
        int x = Mathf.RoundToInt(pacmanPos.x);
        int y = Mathf.RoundToInt(pacmanPos.y);

        GameObject tileObj = _board.GetTileAt(x, y);
        if (tileObj == null) return false;

        var tile = tileObj.GetComponent<Tile>();
        if (tile != null && tile.isBonusItem)
        {
            int score = tile.pointValue;

            if (isPlayerOne)
                GameBoardView.currentPlayerScore += score;

            GameObject.Find("Game").GetComponent<GameBoardView>()
                .StartConsumedBonusItem(tile.gameObject, score);

            return true;
        }

        return false;
    }
}
