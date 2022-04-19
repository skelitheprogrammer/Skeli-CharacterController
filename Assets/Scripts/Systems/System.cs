public abstract class GameSystem
{
    protected bool _enabled = true;

    public void Toggle()
    {
        _enabled = !_enabled;
    }

    public void Toggle(bool state)
    {
        _enabled = state;
    }

    public abstract void Procceed();
}
