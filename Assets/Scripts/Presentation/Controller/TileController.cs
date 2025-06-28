using UnityEngine;

public enum TilesValues { PelletValue = 10, BonusItemValue = 300 }
public class TileController : MonoBehaviour
{
    [Header("Configuración")]
    public bool isPortal;
    public bool isPellet;
    public bool isSuperPellet;
    public bool isBonusItem;
    public int pointValue;

    [Header("Conexiones")]
    public GameObject portalReceiver;
    public TileEntity _tile;

    private void Awake()
    {
        var position = new Position(transform.position);
        var pellet = isPellet || isSuperPellet ? new PelletEntity((int)TilesValues.PelletValue, isSuperPellet) : null;
        var bonusItem = isBonusItem ? new BonusItemEntity((int)TilesValues.BonusItemValue) : null;
        _tile = new TileEntity(position, pellet, bonusItem, isPortal);
    }

    public TileEntity ToEntity()
    {
        var position = new Position(transform.position);
        var pellet = isPellet || isSuperPellet ? new PelletEntity(10, isSuperPellet) : null;
        var bonus = isBonusItem ? new BonusItemEntity(pointValue) : null;

        return new TileEntity(position, pellet, bonus, isPortal);
    }

}
