using UnityEngine;

public class PlayerRotationController : PlayerRotationControllerBase
{
    private readonly FreeFormRotationModule _freeFormRotation;
    private readonly StrafeRotationModule _strafeRotation;
    private readonly InputReader _input;
    private readonly StateMachineTickable _stateMachine;
    
    private IRotationModule _currentRotationSystem;

    public PlayerRotationController(FreeFormRotationModule freeFormRotation, StrafeRotationModule strafeRotation, InputReader input, StateMachineTickable stateMachineTickable)
    {
        _stateMachine = stateMachineTickable;
        _freeFormRotation = freeFormRotation;
        _strafeRotation = strafeRotation;
        _input = input;

        var stateBuilder = new StateBuilder();
        var stateMachineBuilder = new StateMachineBuilder();

        StateMachine stateMachine = stateMachineBuilder.Begin("Rotation StateMachine").Build();

        State freeFormS = stateBuilder.Begin("FreeForm")
            .BuildLogic()
                .WithEnter(() => SetRotation(_freeFormRotation))
            .Build();

        State strafeS = stateBuilder.Begin("Strafe")
            .BuildLogic()
                .WithEnter(() => SetRotation(_strafeRotation))
            .Build();

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


    public void SetRotation(IRotationModule system)
    {
        _currentRotationSystem = system;
    }
}
