using UnityEngine;

public class NodeController : MonoBehaviour
{
    [Tooltip("Vecinos visibles en Unity")]
    public NodeController[] neighborControllers;
    public NodeEntity NodeEntity { get; set; }
    private IPosition _position;
    public IPosition Position
    {
        get
        {
            if (_position == null)
                _position = new Position(transform.position);
            return _position;
        }
    }

    private void Awake()
    {
        _position = new Position(transform.position);
    }

    public NodeEntity ToEntity()
    {
        if (NodeEntity == null)
            Debug.LogError($"{gameObject.name}: NodeEntity no ha sido inicializado.");
        return NodeEntity;
    }
}
