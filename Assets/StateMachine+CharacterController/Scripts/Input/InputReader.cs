using UnityEngine;
using UnityEngine.InputSystem;

public class InputReader : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public Vector3 MoveInputDirection => new Vector3(MoveInput.x, 0, MoveInput.y).normalized;
    public Vector2 RotateInput { get; private set; }

    public float CameraScroll { get; private set; }

    public bool IsJumped => _map.Movement.Jump.WasPerformedThisFrame();
    public bool SwitchMode => _map.Movement.SwitchModes.WasPerformedThisFrame();
    public bool JumpInput { get; private set; }

    private PlayerInputMap _map;

    private void Awake()
    {
        _map = new();
        SetCursorMode(CursorLockMode.Locked);        
    }

    private void OnEnable()
    {
        _map.Enable();

        _map.Movement.Move.performed += OnMove;
        _map.Movement.Move.canceled += OnMove;

        _map.Movement.Rotate.performed += OnRotate;
        _map.Movement.Rotate.canceled += OnRotate;

        _map.Movement.CameraZoom.performed += OnCameraScroll;
        _map.Movement.CameraZoom.canceled += OnCameraScroll;


        _map.Movement.Jump.performed += OnJump;
        _map.Movement.Jump.canceled += OnJump;
    }

    private void OnDisable()
    {
        _map.Movement.Move.performed -= OnMove;
        _map.Movement.Move.canceled -= OnMove;

        _map.Movement.Rotate.performed -= OnRotate;
        _map.Movement.Rotate.canceled -= OnRotate;

        _map.Movement.CameraZoom.performed -= OnCameraScroll;
        _map.Movement.CameraZoom.canceled -= OnCameraScroll;

        _map.Movement.Jump.performed -= OnJump;
        _map.Movement.Jump.canceled -= OnJump;

        _map.Disable();
    }

    private void OnMove(InputAction.CallbackContext ctx) => MoveInput = ctx.ReadValue<Vector2>();
    private void OnRotate(InputAction.CallbackContext ctx) => RotateInput = ctx.ReadValue<Vector2>();
    private void OnJump(InputAction.CallbackContext ctx) => JumpInput = ctx.ReadValueAsButton();
    private void OnCameraScroll(InputAction.CallbackContext ctx) => CameraScroll = ctx.ReadValue<float>();

    public void SetCursorMode(CursorLockMode mode) => Cursor.lockState = mode;
}
