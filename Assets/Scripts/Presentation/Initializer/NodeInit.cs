using System.Linq;
using UnityEngine;

public class NodeInit : MonoBehaviour
{
    public NodeEntity[] Initialize()
    {
        var nodeControllers = GameObject.FindObjectsOfType<NodeController>();

        // 1. Crear NodeEntity base
        foreach (var ctrl in nodeControllers)
        {
            if (ctrl == null)
            {
                Debug.LogError("❌ Un NodeController es null.");
                continue;
            }

            if (ctrl.Position == null)
            {
                Debug.LogError($"❌ {ctrl.name} tiene Position null.");
                continue;
            }

            int neighborCount = ctrl.neighborControllers != null ? ctrl.neighborControllers.Length : 0;
            ctrl.NodeEntity = new NodeEntity(ctrl.Position, neighborCount);
            ctrl.NodeEntity.DebugName = ctrl.name;
        }

        // 2. Asignar vecinos y direcciones
        foreach (var ctrl in nodeControllers)
        {
            if (ctrl.NodeEntity == null)
            {
                Debug.LogError($"❌ {ctrl.name}: NodeEntity no fue inicializado.");
                continue;
            }

            int neighborCount = ctrl.neighborControllers != null ? ctrl.neighborControllers.Length : 0;
            var neighbors = new NodeEntity[neighborCount];

            for (int i = 0; i < neighborCount; i++)
            {
                var neighborCtrl = ctrl.neighborControllers[i];

                if (neighborCtrl == null)
                {
                    Debug.LogWarning($"⚠️ {ctrl.name}: neighborControllers[{i}] es null.");
                    continue;
                }

                if (neighborCtrl.NodeEntity == null)
                {
                    Debug.LogWarning($"⚠️ {ctrl.name}: NodeEntity del vecino {neighborCtrl.name} es null.");
                    continue;
                }

                neighbors[i] = neighborCtrl.NodeEntity;
            }

            ctrl.NodeEntity.SetNeighborsAndDirections(neighbors);
        }

        return nodeControllers.Select(c => c.NodeEntity).ToArray();
    }
}
