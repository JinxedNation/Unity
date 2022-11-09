using System;
using System.Data;
using UnityEngine;

/**
 * @class Collectable
 * @brief Class to be added as a component to objects that are clickable.
 *
 * @author Chase Percy
 * @date 26/04//2022
 *
 */
public class Collectable : MonoBehaviour, IClickable
{
    /**
     * Represents the types of items a collectable can be
     */
    public enum ItemType
    {
        Natural,
        Plastic,
        Waste,
        Aluminium,
        Glass
    }

    [SerializeField] [Tooltip("The description of the clickable item")]
    private Dialogue dialogue;

    [SerializeField] [Tooltip("An image of the item to be displayed in the UI once collected")]
    private Sprite itemImage;

    [SerializeField] [Tooltip("The name of the item")]
    private string itemName;

    [SerializeField] [Tooltip("The items material type")]
    private ItemType itemType;

    [SerializeField]
    [Tooltip("An educational dialogue about why this item isn't waste. Required if Item Type is Natural")]
    private Dialogue naturalItemDialogue;

    private void Start()
    {
        if (naturalItemDialogue == null && itemType == ItemType.Natural)
        {
            throw new NoNullAllowedException("A natural item must have a educational dialogue");
        }
    }

    /**
     * Called when a clickable item is clicked.
     * @param clickType the type of click the object received
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
            Collected.Instance.Add(this);
            FindObjectOfType<UIManager>().UpdateImage(itemName, itemImage);
        }
    }

    /**
     * Get the items image
     */
    public Sprite GetImage()
    {
        return itemImage;
    }

    /**
     * get the items name
     */
    public string GetName()
    {
        return itemName;
    }

    /**
     * Return the collectables item type
     */
    public ItemType GetItemType()
    {
        return itemType;
    }

    /**
     * Return the natural dialogue for the item
     */
    public Dialogue GetNaturalDialogue()
    {
        return naturalItemDialogue;
    }

    public void DontDestroy()
    {
        DontDestroyOnLoad(GetComponentInParent<Transform>().gameObject);
    }
}