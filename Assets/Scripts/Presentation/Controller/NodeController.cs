using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeController : MonoBehaviour
{
    [Tooltip("Vecinos visibles en Unity")]
    public NodeController[] neighborControllers;

    public NodeEntity NodeEntity { get; private set; }

    private IPosition _position;

    private void Awake()
    {
        _position = new Position(transform.position);
    }

    public void Initialize()
    {
        // Ya debería haberse ejecutado Awake() de todos los nodos
        var neighborEntities = new NodeEntity[neighborControllers.Length];

        for (int i = 0; i < neighborControllers.Length; i++)
        {
            if (neighborControllers[i] != null)
            {
                neighborEntities[i] = neighborControllers[i].NodeEntity;
            }
        }

        NodeEntity = new NodeEntity(_position, neighborEntities);
    }

    public NodeEntity ToEntity()
    {
        return this.NodeEntity;
    }
}
