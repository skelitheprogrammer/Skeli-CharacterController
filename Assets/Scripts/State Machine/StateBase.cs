namespace Skeli.StateMachine
{
    public abstract class StateBase
    {
        public readonly string Name;

        public StateBase() => Name = $"EmptyState_{GetHashCode()}";
        public StateBase(string name) => Name = name;
    }

}