using UnityEngine;
using Zenject;

public class PlayerRotationController : PlayerRotationControllerBase
{
    private readonly PlayerFreeFormRotationSystem _freeFormRotation;
    private readonly PlayerStrafeRotationSystem _strafeRotation;
    private readonly InputReader _input;

    private IPlayerRotationSystem _currentRotationSystem;

    private readonly StateMachineTickable _stateMachine;

    public PlayerRotationController(PlayerFreeFormRotationSystem freeFormRotation, PlayerStrafeRotationSystem strafeRotation, InputReader input, StateMachineTickable stateMachineTickable)
    {
        _stateMachine = stateMachineTickable;
        _freeFormRotation = freeFormRotation;
        _strafeRotation = strafeRotation;
        _input = input;

        var stateBuilder = new State.StateBuilder();
        var stateMachineBuilder = new StateMachine.StateMachineBuilder();
        StateMachine stateMachine = stateMachineBuilder.Begin("Rotation StateMachine").Build();

        State freeFormS = stateBuilder.Begin("FreeForm").BuildEnter(() => SetRotation(_freeFormRotation)).Build();
        State strafeS = stateBuilder.Begin("Strafe").BuildEnter(() => SetRotation(_strafeRotation)).Build();

        stateMachine.AddState(freeFormS);
        stateMachine.AddState(strafeS);

        stateMachine.AddTransition(new Transition(freeFormS, strafeS, () => _input.SwitchMode && stateMachine.ActiveState == freeFormS));
        stateMachine.AddTransition(new Transition(strafeS, freeFormS, () => _input.SwitchMode && stateMachine.ActiveState == strafeS));

        stateMachine.Init(freeFormS);
        _stateMachine.SetStateMachine(stateMachine);
    }

    public override Quaternion CalculatePlayerRotation()
    {
        return _currentRotationSystem.CalculateRotationAngle();
    }


    public void SetRotation(IPlayerRotationSystem system)
    {
        _currentRotationSystem = system;
    }
}
