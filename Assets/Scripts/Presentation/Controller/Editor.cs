using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Zenject;

public class AddNodeControllers
{
    [MenuItem("Tools/Zenject/Add DITileViews to GameObjectContext Installers")]
    private static void AddDITileViewsInstaller()
    {
        int modifiedCount = 0;

        foreach (var obj in Selection.gameObjects)
        {
            var context = obj.GetComponent<GameObjectContext>();
            var installer = obj.GetComponent<DITileViews>();

            if (context != null && installer != null)
            {
                var installersList = new System.Collections.Generic.List<MonoInstaller>(context.Installers);

                if (!installersList.Contains(installer))
                {
                    installersList.Add(installer);
                    context.Installers = installersList.ToArray();
                    EditorUtility.SetDirty(context);
                    modifiedCount++;
                }
            }
        }

        Debug.Log($"✅ Se añadieron DITileViews a {modifiedCount} GameObjectContext(s) seleccionados.");
    }

    [MenuItem("Tools/Zenject/Add DITileViews to GameObjectContext Installers", true)]
    private static bool ValidateSelection()
    {
        foreach (var obj in Selection.gameObjects)
        {
            if (obj.GetComponent<GameObjectContext>() != null && obj.GetComponent<DITileViews>() != null)
                return true;
        }
        return false;
    }
}