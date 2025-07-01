using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPellet : MonoBehaviour
{
    private readonly SpriteRenderer _renderer;
    public ShowPellet(SpriteRenderer renderer)
    {
        _renderer = renderer;
    }
    public void SetVisible(bool visible)
    {
        if (_renderer != null)
            _renderer.enabled = visible;
    }
}
