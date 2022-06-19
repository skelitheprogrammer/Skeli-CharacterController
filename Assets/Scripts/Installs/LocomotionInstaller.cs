using Skeli.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LocomotionInstaller : LifetimeScope
{
    [SerializeField] private CharacterController _controller;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<StateMachineContext>(Lifetime.Singleton);
        builder.RegisterComponent(_controller);

        builder.Register<GravitySystem>(Lifetime.Singleton);

        builder.Register<FreeFormMovementModule>(Lifetime.Singleton);
        builder.Register<AirControlModule>(Lifetime.Singleton);
        builder.Register<StrafeMovementModule>(Lifetime.Singleton);

        builder.Register<MovementService>(Lifetime.Singleton);

        builder.Register<LocomotionMovementController>(Lifetime.Singleton);

        builder.Register<FreeFormRotationModule>(Lifetime.Singleton);
        builder.Register<StrafeRotationModule>(Lifetime.Singleton);

        builder.Register<RotationService>(Lifetime.Singleton);

        builder.Register<LocomotionRotationController>(Lifetime.Singleton);
    }
}
