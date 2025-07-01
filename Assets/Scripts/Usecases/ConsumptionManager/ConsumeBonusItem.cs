public class ConsumeBonusItem : IStrategyConsume
{
    private ISubjectGame _gameBoard;
    public ConsumeBonusItem(ISubjectGame game)
    {
        _gameBoard = game;
    }
    public bool Consume(PacmanEntity pacman, TileEntity tile)
    {
        if (tile.IsEmpty || tile.IsPortal)
        {
            return false;
        }
        int value = tile.BonusItem.Value;
        _gameBoard.Score += value;
        tile.ToEmpty();
        return true;
    }

}
