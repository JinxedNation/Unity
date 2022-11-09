using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UnityEngine.Events;

/**
 * @class ObjectSelection
 * @brief This class checks if shopping items can be selected
 *          Then makes them red if they can be picked up and return original color if not selected
 *
 * @author Michael John
 * @version 01
 * @date 18/05/2022
 *
 */
public class ObjectSelection : MonoBehaviour
{
    [Tooltip("The event called when the a new shopping item\nis targeted")]
    public UnityEvent<GameObject> onSelected;

    [Tooltip("The event called when no shopping item\nis targeted")]
    public UnityEvent<GameObject> onDeSelected;

    [Tooltip("Set the radius of when Items become selectable")]
    public float detectionRadius = 1;

    private Collider closestCollider = null; 
    private GameObject closestObject = null;

    void Update()
    {
        GetClosestPickUpItem();
    }

    /**
     * Determines the closest item tot he player if multiple are within the detection radius
     */
    void GetClosestPickUpItem()
    {
        Collider[] itemColliders = Physics.OverlapSphere(this.transform.position, detectionRadius);

        //prime the loop first distaance
        float closestColliderDistance = Mathf.Infinity;
        //store the loops closest object
        Collider newClosestCollider = null;

        foreach (var itemCollider in itemColliders)
        {
            if(itemCollider.tag == "CanPickUp")
            {
                float newDistance = Vector3.Distance(itemCollider.transform.position, this.transform.position);
                if(newDistance < closestColliderDistance)
                {
                    closestColliderDistance = newDistance;
                    newClosestCollider = itemCollider;
                }
            }
        }

        if (newClosestCollider == null)
        {
            if (closestCollider != null)
            {
                //this is because its not initialised on startup
                //set old to original
                //SetColliderColor(closestCollider, Color.red);
                closestCollider.GetComponent<ShoppingItem>().OnDeselected();
            }

            this.onDeSelected.Invoke(this.closestObject);

            closestCollider = null;
            closestObject = null;

        }
        else if (closestCollider != newClosestCollider )//not same
        {
            if (closestCollider != null)
            {
                //this is because its not initialised on startup
                //set old to original color
                closestCollider.GetComponent<ShoppingItem>().OnDeselected();
            }

            //set new to selected
            //SetColliderColor(newClosestCollider, Color.green);
            newClosestCollider.GetComponent<ShoppingItem>().OnSelected();

            closestCollider = newClosestCollider;
            //closestObject = closestCollider.transform.parent.gameObject;
            closestObject = closestCollider.gameObject;
            //this.onSelected.Invoke(closestCollider);
            this.onSelected.Invoke(this.closestObject);
        }
    }

    void SetColliderColor(Collider collider, Color newColor)
    {
        if(collider != null)
        {
            var colliderRender = collider.GetComponent<Renderer>();
            colliderRender.material.SetColor("_Color", newColor);
        }
    }

}
