using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class AddNodeControllers
{
    [MenuItem("Pacman/Asignar Vecinos solo a NonNodes %#n")] // Ctrl+Shift+N
    private static void AssignNeighbors()
    {
        // Buscar el objeto raíz "NonNodes" en la escena
        GameObject nonNodesRoot = GameObject.Find("NonNodes");
        if (nonNodesRoot == null)
        {
            Debug.LogError("No se encontró el objeto raíz 'NonNodes' en la jerarquía.");
            return;
        }

        // Obtener todos los hijos activos con NodeController
        NodeController[] nodeControllers = nonNodesRoot.GetComponentsInChildren<NodeController>(true);

        Dictionary<int, NodeController> pelletMap = new Dictionary<int, NodeController>();

        // Regex para detectar "pellet (i)"
        Regex regex = new Regex(@"pellet \((\d+)\)", RegexOptions.IgnoreCase);

        // Mapear nodos según índice i en nombre "pellet (i)"
        foreach (var node in nodeControllers)
        {
            var match = regex.Match(node.gameObject.name);
            if (match.Success && int.TryParse(match.Groups[1].Value, out int index))
            {
                pelletMap[index] = node;
            }
        }

        int assignedCount = 0;

        // Asignar vecinos i-1 e i+1
        foreach (var kvp in pelletMap)
        {
            int index = kvp.Key;
            NodeController currentNode = kvp.Value;

            List<NodeController> neighbors = new List<NodeController>();

            if (pelletMap.TryGetValue(index - 1, out var prev))
                neighbors.Add(prev);

            if (pelletMap.TryGetValue(index + 1, out var next))
                neighbors.Add(next);

            currentNode.neighborControllers = neighbors.ToArray();
            EditorUtility.SetDirty(currentNode);
            assignedCount++;
        }

        Debug.Log($"✅ Vecinos asignados automáticamente a {assignedCount} NodeControllers bajo 'NonNodes'.");

    }
}