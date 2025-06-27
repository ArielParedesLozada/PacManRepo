public class TileEntity
{
    public IPosition Position { get; set; }
    public BonusItemEntity BonusItem { get; set; }
    public PelletEntity Pellet { get; set; }
    public bool IsEmpty { get; set; }
    public bool IsPortal { get; set; }

    public void ToEmpty()
    {
        BonusItem = null;
        Pellet = null;
        IsEmpty = true;
    }
}
