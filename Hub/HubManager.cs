using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @class HubManager
 * @brief Class to manage Hub related functionality, i.e. particle system colours, Dialogue, and Eventually AI related activities.
 *
 * @author Chase Percy
 * @version 01
 * @date 22/03/2022
 *
 */
public class HubManager : DialogueTrigger
{
    /// The particle system game object
    [Tooltip("The particle system that represents pollution.")] [SerializeField]
    private GameObject particleSystemObj;

    /// The initial button to select when using gamepad.
    [Tooltip("The initial button to select when in the hub (Gamepad)")] [SerializeField]
    private GameObject initialButton;

    [SerializeField] [Tooltip("The dialogue to queue on start")]
    public Dialogue dialogue;

    private static bool _welcomePlayed;

    /**
     *  Calls Controller Utility to select the button if the controller is in use.
     */
    private void Start()
    {
        OnMenuClose();
        if (!_welcomePlayed)
        {
            QueueDialogue(dialogue);
            _welcomePlayed = true;
        }
    }

    /**
     *  Calls Controller Utility to select the button if the controller is in use and set the hub button select.
     */
    public void OnMenuClose()
    {
        ControllerUtility.SelectGameObject(initialButton);
    }

    /**
     *  Sets the  colour of the particle system
     *  TODO: Set the pollution progress meter to the same colour
     *
     *  @param color the new color to use
     */
    public void setColour(Color color)
    {
        var ps = particleSystemObj.GetComponent<ParticleSystem>();

        var main = ps.main;
        main.startColor = color;
    }
}