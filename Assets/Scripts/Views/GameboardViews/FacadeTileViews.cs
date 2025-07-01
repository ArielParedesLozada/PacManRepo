using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FacadeTileViews : MonoBehaviour
{
    [Inject] public TileController tileController;
    [Inject] private ShowPellet _showPellet;

    void Update()
    {
        if (tileController != null && tileController._tile != null)
        {
            bool isEmpty = tileController._tile.IsEmpty;
            Debug.Log($"SKIBIDI soy la tile {tileController._tile.DebugName}, asignada a {tileController.name} y estoy consumida: {isEmpty}");
            _showPellet.SetVisible(!isEmpty);
        }
    }
}
