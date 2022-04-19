using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector3 MoveInputDirection => new Vector3(MoveInput.x, 0, MoveInput.y).normalized;
    public Vector2 RotateInput { get; private set; }

    public bool IsJumped => _jumpInput.action.WasPerformedThisFrame();
    public bool JumpInput { get; private set; }

    [SerializeField] private InputActionReference _moveAction;
    [SerializeField] private InputActionReference _rotateAction;
    [SerializeField] private InputActionReference _jumpInput;

    private void OnEnable()
    {
        _moveAction.action.performed += OnMove;
        _moveAction.action.canceled += OnMove;

        _rotateAction.action.performed += OnRotate;
        _rotateAction.action.canceled += OnRotate;

        _jumpInput.action.performed += OnJump;
        _jumpInput.action.canceled += OnJump;
    }

    private void OnDisable()
    {
        _moveAction.action.performed -= OnMove;
        _moveAction.action.canceled -= OnMove;

        _rotateAction.action.performed -= OnRotate;
        _rotateAction.action.canceled -= OnRotate;

        _jumpInput.action.performed -= OnJump;
        _jumpInput.action.canceled -= OnJump;
    }

    private void OnMove(InputAction.CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>();
    private void OnRotate(InputAction.CallbackContext ctx) => RotateInput = ctx.ReadValue<Vector2>();
    private void OnJump(InputAction.CallbackContext ctx) => JumpInput = ctx.ReadValueAsButton();

}
