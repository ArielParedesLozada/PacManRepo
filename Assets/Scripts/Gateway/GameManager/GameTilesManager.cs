using UnityEngine;

public class GameTilesManager
{
    private TileEntity[] _tiles;
    public void Initialize(TileEntity[] tiles)
    {
        _tiles = tiles;
    }
    public TileEntity GetTileAt(IPosition position)
    {
        for (int i = 0; i < _tiles.Length; i++)
        {
            if (_tiles[i].Position.Equals(position))
            {
                return _tiles[i];
            }
        }
        return null;
    }
}
