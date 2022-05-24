using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstall : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<Skeli.StateMachine.StateMachineContext>().AsTransient().NonLazy();
        //Container.BindInterfacesAndSelfTo<StateMachineTickable>().AsCached().NonLazy();
    }
}