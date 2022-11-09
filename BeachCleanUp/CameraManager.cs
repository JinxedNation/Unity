using UnityEngine;
using UnityEngine.InputSystem;

/**
 * @class CameraManager
 * @brief Class to manage the camera in the Beach cleanup scene.
 *
 * @author Chase Percy
 * @date 25/04/2022
 *
 */
public class CameraManager : MonoBehaviour
{
    [SerializeField] [Tooltip("The Cameras movement speed")]
    private float speed = 5;

    [SerializeField] [Tooltip("The component to get player input from")]
    private PlayerInput input;

    [SerializeField] [Tooltip("Name of the action to get movement from")]
    private string moveName = "Player/Move";

    /**
     * Input Action relating the player movement
     */
    private InputAction _moveAction;

    private void Awake()
    {
        _moveAction = input.actions[moveName];
    }

    private void Update()
    {
        Move(_moveAction.ReadValue<Vector2>());
    }

    /**
     * Moves the camera relative to direction.
     * @param direction the normalised direction vector
     */
    private void Move(Vector2 direction)
    {
        var dSpeed = speed * Time.deltaTime;
        var ogPos = transform.position;
        transform.Translate(new Vector3(dSpeed * direction.x, 0, dSpeed * direction.y));
        if (transform.position.x > 311 || transform.position.x < -61 || transform.position.y > 122 ||
            transform.position.y < 60)
            transform.position = ogPos;
    }
}