using System.Collections.Generic;
using UnityEngine;

/**
 * A helper script which queues some dialogue on start
 * @author Chase Percy 33986429
 * @author Rhys Mader 33705134
 * @since 29 April 2022
 */
public class StartUpDialogue : DialogueTrigger
{
    [SerializeField]
    [Tooltip("The dialogue to queue on start")]
    public List<Dialogue> dialogues;

	void Start()
    {
        foreach (var dialogue in dialogues)
        {
            this.QueueDialogue(dialogue);
        }
    }
}
