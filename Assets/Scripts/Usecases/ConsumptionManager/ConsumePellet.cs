public class ConsumePellet : IStrategyConsume
{
    private ISubjectGame _gameBoard;

    public ConsumePellet(ISubjectGame game)
    {
        _gameBoard = game;
    }
    public bool Consume(PacmanEntity pacman, TileEntity tile)
    {
        if (tile.IsEmpty || tile.IsPortal)
        {
            return false;
        }
        if (tile.Pellet.IsSuperPellet)
        {
            pacman.Empower();
            _gameBoard.Notify();
        }
        int value = tile.Pellet.Value;
        _gameBoard.Score += value;
        tile.ToEmpty();
        return true;
    }
}
