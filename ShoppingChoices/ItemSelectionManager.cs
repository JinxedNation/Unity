using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


/**
 * @class GameManagementx
 * @brief This class handles switching on and off pickup/drop buttons and displaying the selected items information.
 *          ALso responsible for placing selected item into the players hand
 *
 * @author Michael John
 * @version 01
 * @date 18/05/2022
 *
 */
public class ItemSelectionManager : MonoBehaviour
{
    [SerializeField] [Tooltip("The currently selectedItem")]
    private GameObject selectedItem;

    [Tooltip("Pickup Button to made active/inactive")]
    public GameObject PickUpButton;

    [Tooltip("Drop Button to made active/inactive")]
    public GameObject DropButton;

    [Tooltip("Item selecter on player to detect the closest item for inspection and pickup")]
    public GameObject ItemSelector;

    [Tooltip("Gameobject to be made parent on pickup")]
    public Transform holdPos;

    [Tooltip("Inspection panel to be made active/inactive")]
    public GameObject InspectBackground;

    [Tooltip("Sprite container to display the items label")]
    public Image LabelContainer;

    [Tooltip("Item name text box to be made active/inactive")]
    public Text ItemNameText;

    [Tooltip("Item materials text box to be made active/inactive")]
    public Text MaterialText;

    [Tooltip("The event called when the a new shopping item\nis targeted")]
    public UnityEvent onPickUp;


    //displays the selected item into the panel and colours red for identification
    public void ItemSelected(GameObject item)
    {
        selectedItem = item;
        PickUpButton.SetActive(true);
        ControllerUtility.SelectGameObject(PickUpButton);
        //InspectButton.SetActive(true);

        var ItemScript = selectedItem.GetComponent<ShoppingItem>();
        Sprite label = ItemScript.Label;
        if (label != null)
        {
            //do something
            InspectBackground.SetActive(true);
            LabelContainer.sprite = label;
            ItemNameText.text = ItemScript.ItemName;
            MaterialText.text = ItemScript.MaterialString();
        }
        else
        {
            //fail, remove display, report error
            Debug.Log("null item label sprite");
        }
    }

    public void NoItemSelected(GameObject item)
    {
        selectedItem = item;
        PickUpButton.SetActive(false);
        InspectBackground.SetActive(false);
        LabelContainer.sprite = null;
        ItemNameText.text = "Inspection Inactive";
    }

    /**
     * PLaces the selected item into the players hand
     */
    public void PickUpItem()
    {
        this.onPickUp.Invoke();
        PickUpButton.SetActive(false);
        DropButton.SetActive(true);
        ControllerUtility.SelectGameObject(DropButton);
        //selectedItem.GetComponent<BoxCollider>().enabled = false;
        selectedItem.GetComponent<Collider>().enabled = false;
        selectedItem.GetComponent<Rigidbody>().useGravity = false;
        selectedItem.transform.position = holdPos.position;

        //selectedItem.transform.parent = holdPos.parent;
        ItemSelector.SetActive(false);
    }

    private void Update()
    {
        if (!ItemSelector.activeSelf)
        {
            selectedItem.transform.position = holdPos.position;
        }
    }

    /**
     * Drops the item from the players hand
     */
    public void DropItem()
    {
        DropButton.SetActive(false);
        //selectedItem.GetComponent("PickUp").DropItem();
        selectedItem.transform.parent = null;
        selectedItem.GetComponent<Rigidbody>().useGravity = true;
        //selectedItem.GetComponent<BoxCollider>().enabled = true;
        selectedItem.GetComponent<Collider>().enabled = true;
        selectedItem = null;
        ItemSelector.SetActive(true);
    }
}