using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class RootInstall : LifetimeScope
{
    [SerializeField] private InputReader _input;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_input);
    }
}
