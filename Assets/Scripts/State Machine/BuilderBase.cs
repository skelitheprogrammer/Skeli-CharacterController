using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BuilderBase<T>
{
    protected T _state;

    public abstract BuilderBase<T> Begin(string name);
    public abstract BuilderBase<T> BuildEnter(Action enter);
    public abstract BuilderBase<T> BuildLogic(Action logic);
    public abstract BuilderBase<T> BuildExit(Action exit);
    public abstract T Build();
}
