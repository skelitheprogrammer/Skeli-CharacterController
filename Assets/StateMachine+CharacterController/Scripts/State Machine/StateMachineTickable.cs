using Skeli.StateMachine;
using System;
using Zenject;

public class StateMachineTickable : ITickable
{
	private StateMachine _stateMachine;

	public void SetStateMachine(StateMachine stateMachine)
    {
		if (_stateMachine != null) throw new InvalidCastException("State machine already setted");

		_stateMachine = stateMachine;
    }

    public void Tick()
    {
		if (_stateMachine == null) return;
		_stateMachine.UpdateState();
    }
}