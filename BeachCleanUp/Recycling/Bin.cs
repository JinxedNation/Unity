using UnityEngine;

/**
 * @class Bin
 * @brief Class to manage the bins in the recycling scene
 *
 * @author Chase Percy
 * @date 27/04//2022
 *
 */
public class Bin : MonoBehaviour, IClickable
{
    [SerializeField] [Tooltip("The waste type the bin is for")]
    private Collectable.ItemType wasteType;

    [SerializeField] [Tooltip("The description of what the bin is for")]
    private Dialogue dialogue;

    [SerializeField] [Tooltip("The message when an item is placed in the wrong bin")]
    private Dialogue wrongBin;

    /**
     * Stops multiple click events per frame
     */
    private float _clickCooldown;

    /**
     * Checks if the collectable can be placed in this bin
     */
    private bool PlaceCollectable(Collectable item)
    {
        return item.GetItemType() == wasteType;
    }

    /**
     * used to update the cooldown
     */
    void Update()
    {
        if (_clickCooldown >= 0) _clickCooldown -= 1 * Time.deltaTime;
    }

    /**
     * Handles when the bin is clicked. Checks if the current item belongs in this bin. If it does it removes it and carries on to the next
     * item. Else it displays a message about what the bin is for and doesn't remove the item.
     */
    public void Clicked(IClickable.ClickType clickType)
    {
        FindObjectOfType<Beach_AudioManager>().Click();
        if (clickType == IClickable.ClickType.Secondary)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        else
        {
            var item = Collected.Instance.GetLast();
            if (item == null || _clickCooldown > 0) return;
            _clickCooldown = 0.5f;

            if (PlaceCollectable(item))
            {
                Collected.Instance.Remove(item);
                item = Collected.Instance.GetLast();
                if (item != null)
                {
                    FindObjectOfType<UIManager_Recycle>().UpdateImage(item.GetName(), item.GetImage());
                }
                else
                {
                    FindObjectOfType<UIManager_Recycle>().UpdateImage(string.Empty, null);
                }
            }
            else
            {
                FindObjectOfType<LevelInterface>().AddMistake();
                FindObjectOfType<DialogueManager>().StartDialogue(wrongBin);
            }
        }
    }
}