public class PelletEntity
{
    public int Value { get; set; }
    public bool IsSuperPellet { get; set; }

    public PelletEntity(int val, bool isSuperPellet)
    {
        IsSuperPellet = isSuperPellet;
        Value = val * (isSuperPellet ? 10 : 1);
    }
}
