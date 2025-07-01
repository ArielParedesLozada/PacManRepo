using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PhantomUseCasesInit : MonoBehaviour
{
    [Inject] private PacmanEntity _pacman;
    [Inject] private ISubjectGame _gameBoard;
    [Inject] private MoveFactory _moveFactory;
    [Inject] private MoveContext _moveContext;
    [Inject] private CollisionFactory _collisionFactory;
    [Inject] private CollisionContext _collisionContext;


    void Start()
    {
        var controllers = FindObjectsOfType<PhantomController>();
        foreach (var controller in controllers)
        {
            var ghostEntity = controller.ToEntity();
            if (ghostEntity == null)
            {
                Debug.LogWarning($"❌ PhantomEntity en {controller.name} es null.");
                continue;
            }

            controller.Initialize(ghostEntity, _moveFactory, _collisionFactory, _moveContext, _collisionContext, _pacman, _gameBoard);
            Debug.Log($"✅ {controller.name} inicializado con estrategia {ghostEntity.State}");
        }
    }
}
