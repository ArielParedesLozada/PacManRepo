using System.Linq;
using UnityEngine;

public class TileInit : MonoBehaviour
{
    public GameTilesManager Initialize()
    {
        var tileControllers = GameObject.FindObjectsOfType<TileController>();

        if (tileControllers.Length == 0)
        {
            Debug.LogWarning("⚠️ No se encontraron TileControllers en la escena.");
        }

        var tileEntities = tileControllers
            .Select(tc =>
            {
                var entity = tc.ToEntity();
                if (entity == null || entity.Position == null)
                {
                    Debug.LogError($"❌ Tile '{tc.name}' devolvió una TileEntity inválida.");
                }
                return entity;
            })
            .Where(e => e != null)
            .ToArray();

        var tileManager = new GameTilesManager();
        tileManager.Initialize(tileEntities);

        Debug.Log($"✅ Inicializadas {tileEntities.Length} TileEntities.");
        return tileManager;
    }
}
