using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(
        typeof(SpriteRenderer),
        typeof(Animator),
        typeof(GhostView)    
    )]
public class GhostController : MonoBehaviour
{
    [Inject] readonly IMoveGhostUseCase _moveUseCase;
    [Inject] readonly GhostEntity _entity;

    GhostView _view;
    Transform _t;
    PacManView _pacView;

    void Awake()
    {
        _t = transform;
        _view = GetComponent<GhostView>();

        // Encontramos la vista de Pac-Man para leer su orientaci�n
        var pacObj = GameObject.FindWithTag("PacMan");
        if (pacObj != null)
            _pacView = pacObj.GetComponent<PacManView>();
    }

    void Update()
    {
        if (!_entity.CanMove) return;

        // 1) Obtenemos posici�n y direcci�n de Pac-Man desde su View
        Vector2 pmPos = Vector2.zero;
        Vector2 pmDir = Vector2.zero;
        if (_pacView != null)
        {
            pmPos = _pacView.transform.localPosition;
            pmDir = _pacView.orientation;
        }

        // 2) Ejecutamos la l�gica de movimiento
        _moveUseCase.Execute(_entity, pmPos, pmDir);

        // 3) Aplicamos la nueva posici�n al Transform
        _t.localPosition = _entity.Position;

        // 4) Actualizamos sprites y animaciones
        ApplyVisuals();
    }

    void ApplyVisuals()
    {
        switch (_entity.CurrentMode)
        {
            case GhostMode.Scatter:
            case GhostMode.Chase:
                // En scatter o chase usamos la animaci�n seg�n la direcci�n
                _view.SetScatterOrChase(_entity.Direction);
                break;

            case GhostMode.Frightened:
                // Alternar azul/blanco al parpadear
                bool white = _entity.FrightenedTimer >= _entity.StartBlinkingAt
                             && ((int)(_entity.FrightenedTimer * 10) % 2 == 0);
                _view.SetFrightened(white);
                break;

            case GhostMode.Consumed:
                // Mostrar s�lo los ojos apuntando en la direcci�n
                _view.SetConsumedEyes(_entity.Direction);
                break;
        }
    }
}
