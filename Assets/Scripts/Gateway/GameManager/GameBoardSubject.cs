public class GameBoardSubject : ISubjectGame
{
    private GameTilesManager _tiles;
    private GameGhostManager _ghostManager;
    private NodeEntity[] _nodes;
    public int Score { get; set; }
    public int Level { get; set; }
    public int Lives { get; set; }
    public NodeEntity GetNodeAt(IPosition position)
    {
        for (int i = 0; i < _nodes.Length; i++)
        {
            if (_nodes[i].Position.Equals(position))
            {
                return _nodes[i];
            }
        }
        return null;
    }

    public TileEntity GetTileAt(IPosition position)
    {
        return _tiles.GetTileAt(position);
    }

    public void Notify()
    {
        _ghostManager.Notify();
    }
}
