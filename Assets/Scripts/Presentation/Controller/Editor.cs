using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AddNodeControllers
{
    [MenuItem("Tools/Check isPellet in Selected Tiles")]
    private static void CheckIsPelletInSelection()
    {
        int totalChecked = 0;
        int totalPellets = 0;

        foreach (var obj in Selection.gameObjects)
        {
            var tileController = obj.GetComponent<TileController>();
            if (tileController != null)
            {
                totalChecked++;
                if (tileController.isPellet)
                {
                    Debug.Log($"🍬 {obj.name} tiene isPellet = true");
                    totalPellets++;
                }
                else
                {
                    Debug.Log($"🧱 {obj.name} tiene isPellet = false");
                }
            }
            else
            {
                Debug.LogWarning($"⚠️ {obj.name} no tiene un TileController asignado.");
            }
        }

        Debug.Log($"🔍 Total revisados: {totalChecked} | Total con isPellet = true: {totalPellets}");
    }

    [MenuItem("Tools/Check isPellet in Selected Tiles", true)]
    private static bool ValidateSelection()
    {
        return Selection.gameObjects.Length > 0;
    }
}