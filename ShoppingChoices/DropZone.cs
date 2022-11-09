using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/**
 * @class DropZone
 * @brief This class checks if shopping items have been dropped off in the drop zone and 
 * places them on the checkout if they are
 *
 * @author Michael John
 * @version 01
 * @date 18/05/2022
 *
 */
public class DropZone : MonoBehaviour
{

    [Tooltip("Game manager containing checklist of items required")]
    [SerializeField] private GameManagement manager;

    [Tooltip("Starting position for collected items")]
    public GameObject CollectionPoint;

    [Tooltip("Manual spacing between collected items")]
    public float DropOffSpacing = 0.5f;

    [Tooltip("Count of correctly collected items")]
    [SerializeField] private int DropOffCount = 0;

    [Tooltip("Count of dropped off items not on the list and discarded")]
    [SerializeField] private int DiscardCount = 0;

    [Tooltip("Name of correctly dropped off items that are stored at checkout")]
    [SerializeField] private List<string> CollectedNamesList;

    [Tooltip("Checks the collected Items are stored above this object when collected")]
    public GameObject ConveyorBelt;

    [Tooltip("Play this sound on correct Item collected")]
    public AudioSource Ding;

    [Tooltip("Play this sound on incorrect Item collected")]
    public AudioSource Error;

    [Tooltip("This event called when the a new shopping item\nis Collides with the drop Zone that is on the checklist")]
    public UnityEvent<GameObject> onCorrectItemDropZone;

    [Tooltip("This event called when a new shopping item\nis Collides with the drop Zone but\n is not on the checklist")]
    public UnityEvent<GameObject> onIncorrectItemDropZone;

    [Tooltip("This event called when a new shopping item\nis Collides with the drop Zone but\n this type has been already collected")]
    public UnityEvent<GameObject> onSameTypeItemDropZone;
    void Start()
    {
        manager = FindObjectOfType<GameManagement>();
    }

    /**
     * If a shopping item is dropped off in drop zone
     * Important to set the collectable items layer to shopping layer and the dropzone has a layer
     * of dropzone. Ensure the in the project settings that shopping items layer and dropzone layer interact
     * and that nothing else does or it will trigger and cause and error.
     */
    void OnTriggerEnter(Collider other)
    {
        string item_Name = other.GetComponent<ShoppingItem>().ItemName;
        Debug.Log("Dropped Items name is: " + item_Name);
        //if this item is not on the checklist and should not be collected
        if (!manager.CheckList.Contains(item_Name))
        {
            this.onIncorrectItemDropZone.Invoke(other.gameObject);
            Debug.Log("This item is not on the list");
            DiscardItem(other);
            if(Error != null && !Error.isPlaying)
            {
                Error.PlayOneShot(Error.clip, 1f);
            }
        }
        else if(manager.CheckList.Contains(item_Name) && CollectedNamesList.Contains(item_Name))
        {
            //Correct item but already have one of these collected
            this.onSameTypeItemDropZone.Invoke(other.gameObject);
            Debug.Log("This item has already been collected");
            DiscardItem(other);
            if (Error != null && !Error.isPlaying)
            {
                Error.PlayOneShot(Error.clip, 1f);
            }
        }
        else if (manager.CheckList.Contains(item_Name))
        {
            //correct item to add
            CollectItem(other);
            Debug.Log("This item is correct and added to the list");
            CollectedNamesList.Add(item_Name);
            manager.ItemDroppedOff(other.gameObject);
            //if the 
            if (Ding != null && !Ding.isPlaying)
            {
                Debug.Log("PLay ding");
                Ding.PlayOneShot(Ding.clip, 1f);
            }
        }

    }

    /**
     * Discard items that aren't required on the shopping list
     */
    private void DiscardItem(Collider other)
    {
        other.GetComponent<Collider>().enabled = false;
        Vector3 position = GameObject.Find("DiscardedItems").transform.position;
        position.x -= DiscardCount * DropOffSpacing;
        other.transform.position = position;
        other.transform.rotation = Quaternion.identity;
        other.transform.parent = GameObject.Find("DiscardedItems").transform;

        other.GetComponent<Collider>().enabled = true;
        Debug.Log("Item Discarded");
        other.tag = "CollectedItem";
        DiscardCount++;
    }

    /**
     * This item is on the list and should be stored on the register for checkout
     */
    private void CollectItem(Collider item)
    {

        item.GetComponent<Collider>().enabled = false;
        Vector3 position = GameObject.Find("GatheredItems").transform.position;
        position.x -= DropOffCount * DropOffSpacing;
        item.transform.position = position;
        item.transform.rotation = Quaternion.identity;
        item.transform.parent = GameObject.Find("GatheredItems").transform;

        item.GetComponent<Collider>().enabled = true;

        //this block is move the object above the conveyor if it is colliding
        var ConveyorCollider = ConveyorBelt.GetComponent<Collider>();
        Vector3 otherPosition = ConveyorBelt.gameObject.transform.position;
        Quaternion otherRotation = ConveyorBelt.gameObject.transform.rotation;
        Vector3 direction;
        float distance;
        bool overlapped = Physics.ComputePenetration(item, transform.position, transform.rotation,
                                            ConveyorCollider, otherPosition, otherRotation,
                                            out direction, out distance
                                        );
        if (overlapped)
        {
            Debug.Log("Is overlapped");
            item.transform.Translate(direction * distance); //move it above the conveyor
        }

        //prevent items falling off conveyor
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        Debug.Log("Item Collected");
        item.tag = "CollectedItem";
        DropOffCount++;
    }


}
