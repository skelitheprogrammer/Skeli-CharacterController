public interface ISystem
{
    bool Enabled { get; }
    void Toggle();
    void Toggle(bool state);
    void Procceed();
}
