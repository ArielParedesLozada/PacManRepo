using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPellet : MonoBehaviour
{
    private SpriteRenderer _renderer;
    void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        if (_renderer == null)
            Debug.LogWarning("⚠️ No se encontró SpriteRenderer en ShowPellet");
    }
    public void SetVisible(bool visible)
    {

        if (_renderer != null)
        {
            _renderer.enabled = visible;
        }
        else
        {
            Debug.Log("No tengo un sprite");
        }
    }
}
