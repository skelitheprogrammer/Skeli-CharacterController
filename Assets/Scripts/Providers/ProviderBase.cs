using UnityEngine;
using Zenject;

public abstract class ProviderBase<T> : MonoBehaviour where T: ISystem
{
    [Inject] private T _system;

    private void Update()
    {
        if (!_system.Enabled) return;
        _system.Procceed();
    }
}
