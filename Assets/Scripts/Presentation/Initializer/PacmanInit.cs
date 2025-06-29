using UnityEngine;

public class PacmanInit : MonoBehaviour
{
    [Header("Configuración")]
    public NodeController startingNodeController;
    public float initialSpeed = 6f;

    [HideInInspector] public PacmanEntity pacmanEntity;
    [HideInInspector] public MovePacman movePacman;

    public void Initialize()
    {
        if (startingNodeController == null)
        {
            Debug.LogError("❌ startingNodeController no está asignado.");
            return;
        }

        NodeEntity startNode = startingNodeController.ToEntity();
        if (startNode == null)
        {
            Debug.LogError("❌ El NodeEntity inicial no fue creado correctamente.");
            return;
        }

        pacmanEntity = new PacmanEntity(startNode, initialSpeed, new Position(1, 1));
        pacmanEntity.SetSpeed(1);
        movePacman = new MovePacman(pacmanEntity);
        Debug.Log("✅ PacmanInit construyó correctamente PacmanEntity y MovePacman.");
        Debug.Log($"Pacman inicia en nodo {pacmanEntity.CurrentNode}");
    }
}
