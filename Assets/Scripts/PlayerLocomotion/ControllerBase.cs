public abstract class ControllerBase<T>
{
    protected T _module;

    public void SetModule(T module)
    {
        _module = module;
    }
}