using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @class DialogueTrigger
 * @brief Triggers a dialogue event
 *
 * @author Chase Percy
 * @version 02
 * @date 23/03/2022
 *
 */
public abstract class DialogueTrigger : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The dialogue manager to use\nLeave null to search scene")]
    protected DialogueManager _manager;

    protected void Awake()
    {
        this._manager ??= FindObjectOfType<DialogueManager>();
    }

    /**
     *  Starts a dialogue using the given Dialogue object
     *  @param dialogue The dialogue to queue
     */
    protected void QueueDialogue(Dialogue dialogue)
    {
        this._manager.StartDialogue(dialogue);
    }
}