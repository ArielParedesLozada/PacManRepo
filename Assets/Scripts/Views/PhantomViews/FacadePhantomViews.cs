using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FacadePhantomViews : MonoBehaviour
{
    [Inject]
    private PhantomController _controller;
    [Inject]
    private PlayMovementScatter _chaseScatter;
    [Inject]
    private PlayMovementFrightened _frightened;
    [Inject]
    private PlayMovementConsumed _consumed;
    void Awake()
    {
        if (_controller == null)
            Debug.LogError("❌ PhantomController no fue inyectado en " + gameObject.name);
        else
            Debug.Log("✅ PhantomController inyectado correctamente en " + gameObject.name);
        _chaseScatter = GetComponent<PlayMovementScatter>();
        _frightened = GetComponent<PlayMovementFrightened>();
        _consumed = GetComponent<PlayMovementConsumed>();
    }

    void Update()
    {
        if (_controller == null || _controller.Phantom == null || _controller.Phantom.Position == null)
        {
            return;
        }
        Vector2 dir = ((Position)_controller.Phantom.Position).ToVector2();
        switch (_controller.Phantom.State)
        {
            case GhostState.Chase:
                ShowChaseScatter(dir);
                break;
            case GhostState.Scatter:
                ShowChaseScatter(dir);
                break;
            case GhostState.Frightened:
                ShowFrightened(false);
                break;
            case GhostState.Consumed:
                ShowConsumed(dir);
                break;
            default:
                break;
        }
    }
    public void ShowChaseScatter(Vector2 dir)
    {
        _chaseScatter?.Play(dir);
    }

    public void ShowFrightened(bool isWhite)
    {
        _frightened?.Play(isWhite);
    }

    public void ShowConsumed(Vector2 dir)
    {
        _consumed?.Play(dir);
    }
}
