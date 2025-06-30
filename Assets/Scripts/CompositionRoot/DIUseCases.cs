using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DIUseCases : MonoInstaller
{
    public override void InstallBindings()
    {
        Debug.Log("⚙️ DIUseCasesInstaller.InstallBindings iniciado");
        // Container.Bind<IStrategyMoveGhost>().To<MoveChase>().AsTransient();
        // Container.Bind<IStrategyMoveGhost>().To<MoveConsumed>().AsTransient();
        // Container.Bind<IStrategyMoveGhost>().To<MoveFrightened>().AsTransient();
        // Container.Bind<IStrategyMoveGhost>().To<MoveScatter>().AsTransient();
        // Container.Bind<IStrategyMoveGhost>().To<MoveStill>().AsTransient();
        Container.Bind<MoveFactory>().AsSingle();
        Container.Bind<PhantomUseCasesInit>()
            .FromNewComponentOnNewGameObject()
            .WithGameObjectName("UseCasesInit")
            .AsSingle()
            .NonLazy();
        Debug.Log("Finaliza creacion de los Usecases");
    }
}
