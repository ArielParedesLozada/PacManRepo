using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileInit : MonoBehaviour
{
    public GameTilesManager Initialize()
    {
        // Encuentra todos los TileControllers activos en la escena
        var tileControllers = GameObject.FindObjectsOfType<TileController>();

        // Convierte cada uno en una TileEntity
        var tileEntities = tileControllers
            .Select(controller => controller.ToEntity())
            .ToArray();

        // Crea el GameTilesManager y le asigna las entidades
        var tileManager = new GameTilesManager();
        tileManager.Initialize(tileEntities); // Este método debes agregarlo si no existe

        return tileManager;
    }
}
