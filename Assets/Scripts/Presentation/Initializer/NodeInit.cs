using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeInit : MonoBehaviour
{
    public NodeEntity[] Initialize()
    {
        NodeController[] nodeControllers = FindObjectsOfType<NodeController>();

        Dictionary<NodeController, NodeEntity> map = new();
        List<NodeEntity> nodeEntities = new();

        // 1. Crear entidades base sin vecinos
        foreach (var ctrl in nodeControllers)
        {
            var pos = new Position(ctrl.transform.position);
            var entity = new NodeEntity(pos, new NodeEntity[0]);
            map[ctrl] = entity;
            nodeEntities.Add(entity);
        }

        // 2. Asignar vecinos (usando los controladores para acceder a sus vecinos visuales)
        foreach (var ctrl in nodeControllers)
        {
            NodeEntity[] neighbors = new NodeEntity[ctrl.neighborControllers.Length];

            for (int i = 0; i < ctrl.neighborControllers.Length; i++)
            {
                neighbors[i] = map[ctrl.neighborControllers[i]];
            }

            // Crear una nueva entidad con vecinos ahora sí
            IPosition pos = new Position(ctrl.transform.position);
            NodeEntity fullEntity = new NodeEntity(pos, neighbors);
            map[ctrl] = fullEntity;
        }

        return nodeEntities.ToArray();
    }
}
