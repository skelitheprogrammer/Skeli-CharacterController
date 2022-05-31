namespace Skeli.StateMachine
{
    public static class StateBuilder
    {
        public static State.Builder Begin() => new();
        public static State.Builder Begin(string name) => new(name);
    }

    public static class StateMachineBuilder
    {
        public static StateMachine.StateMachineBuilder Begin() => new();
        public static StateMachine.StateMachineBuilder Begin(string name) => new(name);
    }
}