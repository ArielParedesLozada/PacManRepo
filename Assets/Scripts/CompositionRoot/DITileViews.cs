using UnityEngine;
using Zenject;

public class DITileViews : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<TileController>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<FacadeTileViews>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<ShowPellet>().FromComponentInChildren().AsSingle();
    }
}
