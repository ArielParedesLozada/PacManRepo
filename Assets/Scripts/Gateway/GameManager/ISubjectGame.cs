public interface ISubjectGame
{
    public int Score { get; set; }
    public void Notify();
    public TileEntity GetTileAt(IPosition position);
    public NodeEntity GetNodeAt(IPosition position);
}
