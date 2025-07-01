using UnityEngine;
using Zenject;

public class FacadeTileViews : MonoBehaviour
{
    [Inject]
    private TileController _tileController;
    private ShowPellet _showPellet;

    void Awake()
    {
        _tileController = GetComponent<TileController>();
        _showPellet = GetComponent<ShowPellet>();

        if (_tileController == null)
            Debug.LogError("❌ No se encontró TileController en FacadeTileViews");

        if (_showPellet == null)
            Debug.LogError("❌ No se encontró ShowPellet en FacadeTileViews");
    }

    void Update()
    {
        if (_tileController != null && _tileController._tile != null)
        {
            bool isEmpty = _tileController._tile.IsEmpty;
            _showPellet.SetVisible(!isEmpty); // Solo mostrar si NO está vacía
        }
    }
}
