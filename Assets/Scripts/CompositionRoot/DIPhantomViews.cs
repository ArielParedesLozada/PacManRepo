using UnityEngine;
using Zenject;

public class DIPhantomViews : MonoInstaller
{
    void Awake()
    {
        Debug.Log("⚙️ DIPhantomViews ejecutándose en " + gameObject.name);
    }
    public override void InstallBindings()
    {
        Container.Bind<PhantomController>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<FacadePhantomViews>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<PlayMovementScatter>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<PlayMovementFrightened>().FromComponentOn(gameObject).AsSingle();
        Container.Bind<PlayMovementConsumed>().FromComponentOn(gameObject).AsSingle();
    }

}
