public interface ISubjectGame
{
    public int Lives { get; set; }
    public int Score { get; set; }
    public int Level { get; set; }
    public void Notify();
    public TileEntity GetTileAt(IPosition position);
    public NodeEntity GetNodeAt(IPosition position);
}
