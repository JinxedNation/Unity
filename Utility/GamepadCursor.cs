using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

// Credit https://www.youtube.com/watch?v=Y3WNwl1ObC8
// Slightly altered to work with unity events and our game.

/**
 * @class GamepadCursor
 * @brief Class to provide mouse like utility for the gamepad
 *
 * @author https://www.youtube.com/watch?v=Y3WNwl1ObC8
 * @author Chase Percy
 * @date 27/04//2022
 *
 */
public class GamepadCursor : MonoBehaviour
{
    [SerializeField] [Tooltip("The input system for the scene")]
    private PlayerInput playerInput;

    [SerializeField] [Tooltip("The new cursors Rect Transform")]
    private RectTransform cursorRectTransform;

    [SerializeField] [Tooltip("The canvas the new cursor is on")]
    private Canvas canvas;

    [SerializeField] [Tooltip("The speed of the cursor")]
    private float cursorSpeed = 1000f;

    [SerializeField] [Tooltip("The padding from the edges of the screen")]
    private float padding = 60f;

    /**
     * The virtual mouse for the gamepad cursor
     */
    private Mouse _virtualMouse;

    /**
     * The mouses previous button state
     */
    private bool _previousMouseState;

    /**
     * The previous control scheme, used as a check when updating
     */
    private string _previousControlScheme = MouseScheme;

    /**
     * The name of the Gamepad scheme within unity
     */
    private const string GamepadScheme = "Gamepad";

    /**
     * The name of the Mouse scheme within unity
     */
    private const string MouseScheme = "Keyboard&Mouse";

    private void Start()
    {
        if (_virtualMouse == null)
        {
            _virtualMouse = (Mouse) InputSystem.AddDevice("VirtualMouse");
        }
        else if (!_virtualMouse.added)
        {
            InputSystem.AddDevice(_virtualMouse);
        }

        InputUser.PerformPairingWithDevice(_virtualMouse, playerInput.user);

        if (cursorRectTransform != null)
        {
            var position = cursorRectTransform.anchoredPosition;
            InputState.Change(_virtualMouse.position, position);
        }

        cursorRectTransform.gameObject.SetActive(ControllerUtility.isGamepadInUse() && !MobileUtility.HandheldInUse());
        if (ControllerUtility.isGamepadInUse() && !MobileUtility.HandheldInUse())
        {
            var centre = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);
            _virtualMouse.WarpCursorPosition(centre);
            InputState.Change(_virtualMouse.position, centre);
        }

        InputSystem.onAfterUpdate += UpdateMotion;
    }

    private void OnDestroy()
    {
        InputSystem.onAfterUpdate -= UpdateMotion;
        if (_virtualMouse != null && _virtualMouse.added) InputSystem.RemoveDevice(_virtualMouse);
        Cursor.visible = true;
    }

    /**
     * Updates the motion of the mouse after the Input System has been updated
     */
    private void UpdateMotion()
    {
        if (_virtualMouse == null || Gamepad.current == null) return;

        var delta = Gamepad.current.rightStick.ReadValue();
        delta *= cursorSpeed * Time.unscaledDeltaTime;

        var currentPosition = _virtualMouse.position.ReadValue();
        var newPosition = currentPosition + delta;

        newPosition.x = Mathf.Clamp(newPosition.x, padding, Screen.width - padding);
        newPosition.y = Mathf.Clamp(newPosition.y, padding, Screen.height - padding);

        InputState.Change(_virtualMouse.position, newPosition);
        InputState.Change(_virtualMouse.delta, delta);

        var buttonPressed = Gamepad.current.buttonSouth.IsPressed();
        if (_previousMouseState != buttonPressed)
        {
            _virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, buttonPressed);
            InputState.Change(_virtualMouse, mouseState);
            _previousMouseState = buttonPressed;
        }

        AnchorCursor(newPosition);
    }

    private void AnchorCursor(Vector2 position)
    {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), position,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main, out anchoredPosition);
        cursorRectTransform.anchoredPosition = anchoredPosition;
    }

    public void OnControlsChanged(PlayerInput input)
    {
        if (input.currentControlScheme == MouseScheme && _previousControlScheme != MouseScheme)
        {
            cursorRectTransform.gameObject.SetActive(false);
            Cursor.visible = true;
            Mouse.current.WarpCursorPosition(_virtualMouse.position.ReadValue());
            _previousControlScheme = MouseScheme;
        }
        else if (playerInput.currentControlScheme == GamepadScheme && _previousControlScheme != GamepadScheme)
        {
            cursorRectTransform.gameObject.SetActive(true);
            Cursor.visible = false;
            InputState.Change(_virtualMouse.position, Mouse.current.position.ReadValue());
            AnchorCursor(Mouse.current.position.ReadValue());
            _previousControlScheme = GamepadScheme;
        }
    }
}