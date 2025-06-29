using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AddNodeControllers
{
    [MenuItem("Pacman/Asignar Vecinos a NonNodes (considerando Nodes también) %#m")] // Ctrl+Shift+M
    private static void AssignMixedNeighborsToNonNodes()
    {
        GameObject nonNodesRoot = GameObject.Find("NonNodes");
        if (nonNodesRoot == null)
        {
            Debug.LogError("No se encontró el objeto 'NonNodes' en la jerarquía.");
            return;
        }

        // --- 1. Obtener todos los NodeControllers en la escena con nombre "pellet (i)"
        NodeController[] allNodes = GameObject.FindObjectsOfType<NodeController>(true);
        Regex regex = new Regex(@"pellet \((\d+)\)", RegexOptions.IgnoreCase);
        Dictionary<int, NodeController> allPellets = new();

        foreach (var node in allNodes)
        {
            var match = regex.Match(node.gameObject.name);
            if (match.Success && int.TryParse(match.Groups[1].Value, out int index))
            {
                allPellets[index] = node;
            }
        }

        // --- 2. Filtrar solo los NodeControllers que estén bajo 'NonNodes'
        NodeController[] nonNodeControllers = nonNodesRoot.GetComponentsInChildren<NodeController>(true);

        int assignedCount = 0;

        foreach (var node in nonNodeControllers)
        {
            var match = regex.Match(node.gameObject.name);
            if (!match.Success || !int.TryParse(match.Groups[1].Value, out int index))
                continue;

            List<NodeController> neighbors = new();

            if (allPellets.TryGetValue(index - 1, out var prev))
                neighbors.Add(prev);

            if (allPellets.TryGetValue(index + 1, out var next))
                neighbors.Add(next);

            node.neighborControllers = neighbors.ToArray();
            EditorUtility.SetDirty(node);
            assignedCount++;
        }

        Debug.Log($"✅ Vecinos mixtos asignados a {assignedCount} NodeControllers bajo 'NonNodes'.");
    }
}