using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AddNodeControllers
{
    [MenuItem("Tools/Remove NodeController from Selected Objects")]
    private static void RemoveNodeControllerFromSelected()
    {
        int removedCount = 0;

        foreach (var obj in Selection.gameObjects)
        {
            var nodeController = obj.GetComponent<NodeController>();
            if (nodeController != null)
            {
                Undo.DestroyObjectImmediate(nodeController);
                removedCount++;
            }
        }

        Debug.Log($"✅ Removidos {removedCount} componentes NodeController de los objetos seleccionados.");
    }

    [MenuItem("Tools/Remove NodeController from Selected Objects", true)]
    private static bool ValidateSelection()
    {
        return Selection.gameObjects.Length > 0;
    }
}