using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AddNodeControllers
{
    [MenuItem("Tools/Add Tile Scripts to Selected Objects")]
    private static void AddScripts()
    {
        int count = 0;

        foreach (var obj in Selection.gameObjects)
        {
            if (obj == null) continue;

            // Agrega ShowPellet si no existe
            if (obj.GetComponent<ShowPellet>() == null)
                obj.AddComponent<ShowPellet>();

            // Agrega FacadeTileViews si no existe
            if (obj.GetComponent<FacadeTileViews>() == null)
                obj.AddComponent<FacadeTileViews>();

            // Agrega DITileViews (tu MonoInstaller para esa tile) si no existe
            if (obj.GetComponent<DITileViews>() == null)
                obj.AddComponent<DITileViews>();

            count++;
        }

        Debug.Log($"✅ Scripts añadidos a {count} objetos seleccionados.");
    }

    [MenuItem("Tools/Add Tile Scripts to Selected Objects", true)]
    private static bool ValidateSelection()
    {
        return Selection.gameObjects.Length > 0;
    }
}