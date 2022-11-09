using System.Collections.Generic;
using UnityEngine;
using Image = UnityEngine.UI.Image;

/**
 * @class TransitionManager
 * @brief Handles the transition between the beach clean and recycling.
 *
 * todo update scoring on transition.
 *
 * @author Chase Percy
 * @date 26/04//2022
 *
 */
public class TransitionManager : DialogueTrigger
{
    [SerializeField] [Tooltip("The Image used as a container for the message")]
    private Image transitionContainer;

    /**
     * Tracks the transitions current state
     */
    private bool _isDisplaying;

    private Queue<Dialogue> _dialogues = new Queue<Dialogue>();

    /**
     * Toggles the transition confirmation message for the beach clean up scene
     */
    public void ToggleMessage()
    {
        _isDisplaying = !_isDisplaying;
        transitionContainer.gameObject.SetActive(_isDisplaying);
    }

    /**
     * Function responsible for transitioning to the recycling scene, it should be used instead of Scene utility as it checks the collected items.
     */
    public void TransitionToRecycling()
    {
        Debug.Log("Game COmplete running");
        ValidateCollectedItems();
        CheckItemsMissed();
        Transition();
    }

    /**
     * Validates the items collected weren't natural, triggers dialogue if they were.
     */
    private void ValidateCollectedItems()
    {
        var collected = Collected.Instance.GetCollected();

        for (var i = 0; i < collected.Count; ++i)
        {
            if (collected[i].GetItemType() != Collectable.ItemType.Natural)
            {
                collected[i].DontDestroy();
                continue;
            }

            FindObjectOfType<LevelInterface>().AddMistake();
            _dialogues.Enqueue(collected[i].GetNaturalDialogue());

            for (var j = i + 1; j < collected.Count; ++j)
                if (collected[i].GetName() == collected[j].GetName())
                    collected.RemoveAt(j--);

            collected.RemoveAt(i--);
        }

        Collected.Instance.SetCollected(collected);
    }

    /**
     * Checks how much waste was left on the beach and deducts points accordingly
     */
    private void CheckItemsMissed()
    {
        var items = FindObjectsOfType<Collectable>();

        foreach (var item in items)
            if (item.GetItemType() != Collectable.ItemType.Natural &&
                item.GetComponent<Transform>().gameObject.activeSelf)
                FindObjectOfType<LevelInterface>().AddMistake();
    }

    /**
     * Transitions from this scene to the Recycling scene
     */
    private void Transition()
    {
        if (_dialogues.Count > 0)
        {
            _manager.Menu.OnCloseComplete.AddListener(ProcessDialogue);
            _manager.StartDialogue(_dialogues.Dequeue());
        }
        else
        {
            FindObjectOfType<LevelInterface>().RunComplete();
            SceneUtility.ExitSceneTo("Recycling");
        }
    }

    /**
     * Processes all the wrong collected item dialogues
     */
    private void ProcessDialogue()
    {
        _manager.Menu.OnCloseComplete.RemoveListener(ProcessDialogue);
        if (_dialogues.Count > 0)
        {
            _manager.Menu.OnCloseComplete.AddListener(ProcessDialogue);
            _manager.StartDialogue(_dialogues.Dequeue());
        }
        else
        {
            Transition();
        }
    }
}