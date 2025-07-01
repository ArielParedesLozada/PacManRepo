using UnityEngine;

public enum TilesValues { PelletValue = 10, BonusItemValue = 300 }

[DisallowMultipleComponent]
public class TileController : MonoBehaviour
{
    [Header("Configuración")]
    public bool isPortal;
    public bool isPellet;
    public bool isSuperPellet;
    public bool isBonusItem;
    public int pointValue = 10;

    [Header("Conexiones")]
    public GameObject portalReceiver;

    [HideInInspector] public TileEntity _tile;

    private void Awake()
    {
        EnsureTileCreated();
    }

    /// <summary>
    /// Retorna la entidad Tile asociada.
    /// Si aún no ha sido creada, la genera.
    /// </summary>
    public TileEntity ToEntity()
    {
        return EnsureTileCreated();
    }

    private TileEntity EnsureTileCreated()
    {
        if (_tile == null)
        {
            var position = new Position(transform.position);

            PelletEntity pellet = null;
            if (isPellet || isSuperPellet)
                pellet = new PelletEntity((int)TilesValues.PelletValue, isSuperPellet);

            BonusItemEntity bonus = null;
            if (isBonusItem)
                bonus = new BonusItemEntity(pointValue);

            _tile = new TileEntity(position, pellet, bonus, isPortal);
            _tile.DebugName = name;
        }

        return _tile;
    }
}
