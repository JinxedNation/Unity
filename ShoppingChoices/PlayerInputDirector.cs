using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/**
 * @class PLayerInputDirector
 * @brief Handles the players movement and animation states
 * 
 * Borrows from Chase Percy's and Rhys' input directors
 * Also incorporates player animations controls from the:
 * SuperCyan animation
 * Character Pack: Free Sample v2.1
 * found on the Unity asset store
 *
 * @author Michael John
 * @version 01
 * @date 18/05/2022
 *
 */
public class PlayerInputDirector : MonoBehaviour
{

    [Header("Controls")]
    [Tooltip("The component to get player input from")]
    public PlayerInput input;

    [Tooltip("Name of the action to get movement from")]
    public string moveName = "Player/Move";

    [Header("Movement speeds")]
    [Tooltip("Players walk speed")]
    [SerializeField] public float Speed = 1.0f;

    [Tooltip("Set the players rotation speed")]
    [SerializeField] public float rotationSpeed = 1.0f;

    private InputAction _moveAction;

    [Tooltip("Players animation")]
    [SerializeField] private Animator m_animator = null;

    [Tooltip("Players rigidbody")]
    [SerializeField] private Rigidbody m_rigidBody = null;

    private readonly string[] m_animations = { "Pickup", "Wave" };

    [Tooltip("If the player is walking for animation")]
    [SerializeField] private bool m_walking;

    private AudioSource footsteps;
    [Header("Audio")]
    [Tooltip("Change the volume of the footsteps")]
    public float footStepVolume = 1f;

    private void Awake()
    {
        this._moveAction = this.input.actions[this.moveName];
        if (!m_animator) { gameObject.GetComponent<Animator>(); }
        if (!m_rigidBody) { gameObject.GetComponent<Animator>(); }

        footsteps = GetComponent<AudioSource>();
    }


    /**
     * call pickup animation
     */
    public void PickUpAnimation()
    {
        m_animator.SetTrigger("Pickup");
    }


    /**
     * Move the player if input 
     */
    private void FixedUpdate()
    {
        m_animator.SetBool("Grounded", true);   //is grounder animation

        Vector2 move = this._moveAction.ReadValue<Vector2>();
        Vector3 movementDirection = new Vector3(move.y, 0, -move.x);
        movementDirection.Normalize();


        Vector3 moveVec = movementDirection * Speed * Time.deltaTime;
        transform.Translate(moveVec, Space.World);


        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
            m_animator.SetFloat("MoveSpeed", Speed);
            //Debug.Log("MoveSpeed " + moveVec.magnitude);
            if (!footsteps.isPlaying)
            {
                footsteps.PlayOneShot(footsteps.clip, footStepVolume);
            }
        }
        else
        {
            m_animator.SetFloat("MoveSpeed", 0f);
            //m_walking = false;
            if (footsteps.isPlaying)
            {
                footsteps.Stop();
            }
        }

    }
}

