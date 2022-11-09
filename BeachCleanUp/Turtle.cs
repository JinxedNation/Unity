using UnityEngine;

/**
 * @class Turtle
 * @brief Class to manage the turtles and their animations/actions
 *
 * @author Chase Percy
 * @date 29/04//2022
 *
 */
public class Turtle : MonoBehaviour
{
    [SerializeField] [Tooltip("The turtles animator")]
    private Animator animator;

    [SerializeField] [Tooltip("The delay before the turtle starts to walk")]
    private float walkDelay;

    /**
     * The object the turtle collided with
     */
    private GameObject _collidedWith;

    /**
     * The seaweed object being eaten
     */
    private GameObject _seaweed;

    /**
     * Sets the turtles animation to walk after the set walkDelay
     */
    void Start()
    {
        Invoke(nameof(Walk), walkDelay);
    }

    /**
     * Checks if the turtle should walk again after a collision
     */
    private void Update()
    {
        if (_collidedWith != null && !_collidedWith.activeSelf)
        {
            _collidedWith = null;
            Walk();
        }
    }

    /**
     * Checks if the turtle collided with waste on the beach.
     */
    private void OnTriggerEnter(Collider other)
    {
        var otherGameObject = other.gameObject;
        if (otherGameObject.CompareTag("Waste"))
        {
            FindObjectOfType<LevelInterface>().AddMistake();
            Hide(otherGameObject);
        }
        else if (otherGameObject.CompareTag("Ocean"))
        {
            Invoke(nameof(DestroyThis), 40f);
        }
        else if (otherGameObject.CompareTag("Seaweed"))
        {
            SetAnimation("Eat");
            _seaweed = otherGameObject;
            _seaweed.layer = 0;
            Invoke(nameof(FinishEatingSeaweed), 5f);
        }
    }

    /**
     * Sets the turtle to walk, used by Invoke
     */
    private void Walk()
    {
        ClearAnimationState();
        animator.SetBool("Walk", true);
    }

    /**
     * Sets the animation and triggers the alert icon and sound
     */
    private void Hide(GameObject otherGameObject)
    {
        SetAnimation("Hide");
        _collidedWith = otherGameObject;
        GetComponent<Alert>().Play();
    }

    /**
     * Clears the current animation state and sets a new one
     */
    private void SetAnimation(string animationName)
    {
        ClearAnimationState();
        animator.SetBool(animationName, true);
    }

    /**
     * Clears oll used animation states
     */
    private void ClearAnimationState()
    {
        animator.SetBool("Hide", false);
        animator.SetBool("Walk", false);
        animator.SetBool("Rest", false);
        GetComponent<Alert>().Stop();
    }

    /**
     * Used to destroy this object via a Invoke call
     */
    private void DestroyThis()
    {
        Destroy(GetComponentInParent<Transform>().gameObject);
    }

    /**
     * Called after a turtle has finished eating the seaweed
     */
    private void FinishEatingSeaweed()
    {
        Walk();
        Destroy(_seaweed);
    }
}