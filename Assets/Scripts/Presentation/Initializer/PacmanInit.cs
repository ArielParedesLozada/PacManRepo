using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacmanInit : MonoBehaviour
{
    [Header("Configuración")]
    public PacmanManager pacmanManager;
    public NodeController startingNodeController;
    public float initialSpeed = 6f;
    // Usa Initialize en vez de Awake
    void Awake()
    {
        Debug.Log("PacmanInit.Awake() fue llamado");
    }
    public void Initialize()
    {
        Debug.Log($"PacmanInit.Initialize() llamado desde {this.gameObject.name}");

        // Intenta asignar PacmanManager si no está en el inspector
        if (pacmanManager == null)
            pacmanManager = FindObjectOfType<PacmanManager>();

        if (pacmanManager == null)
        {
            Debug.LogError("❌ PacmanManager no fue asignado en el Inspector ni encontrado con FindObjectOfType.");
            return;
        }

        if (startingNodeController == null)
        {
            Debug.LogError("❌ startingNodeController no está asignado en PacmanInit.");
            return;
        }

        NodeEntity startNode = startingNodeController.ToEntity();
        if (startNode == null)
        {
            Debug.LogError("❌ El NodeEntity inicial no fue creado correctamente.");
            return;
        }

        // Crea la entidad lógica de Pacman
        PacmanEntity entity = new PacmanEntity(startNode, initialSpeed, new Position(1, 1));
        entity.SetSpeed(1); // O el nivel que corresponda

        // Aquí puedes crear las demás dependencias necesarias (MovePacman, etc.)

        // Inicializa el PacmanManager
        // (Asegúrate de pasar las dependencias correctas)
        pacmanManager.Initialize(
            entity,
            new MovePacman(entity),
            null, // ISubjectGame, pásalo si ya lo tienes
            null  // IStrategyConsume, pásalo si ya lo tienes
        );
        Debug.Log("PacmanInit se inicializó correctamente desde GameInit.");
    }
}
