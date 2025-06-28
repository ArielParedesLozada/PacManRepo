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
    public void Initialize()
    {
        // Si no se asigna manualmente, buscar el PacmanManager en la escena
        if (pacmanManager == null)
            pacmanManager = FindObjectOfType<PacmanManager>();

        // Obtener el nodo inicial desde el NodeController asignado
        NodeEntity startNode = startingNodeController.ToEntity();
        // Crear la entidad lógica de Pacman
        PacmanEntity entity = new PacmanEntity(startNode, initialSpeed, new Position(1, 1));
        entity.SetSpeed(new LevelSetter().GetLevel()); // O el nivel que corresponda

        // Inyectar la entidad en el PacmanManager (puedes agregar más dependencias si lo necesitas)
        pacmanManager.Initialize(
            entity,
            new MovePacman(entity), // O inyecta tu instancia de movimiento
            null,                   // ISubjectGame, inyecta tu instancia real aquí
            null                    // IStrategyConsume, inyecta tu estrategia real aquí
        );
    }
}
