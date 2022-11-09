using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Packaging
{
    Trash,  //bad
    Plastic,    //bad
    NonPlasticRecycle   //Good
}

/**
 * @class ShoppingItem
 * @brief This class Gives characteristics to a shopping item that are assigned by its shelf spawner
 *
 * @author Michael John
 * @version 01
 * @date 18/05/2022
 *
 */
public class ShoppingItem : MonoBehaviour
{
    [Tooltip("This items packaging type")]
    public Packaging Package;

    [Tooltip("This items name assigned by spawner")]
    public string ItemName;

    [Tooltip("This items label assigned by spawner")]
    public Sprite Label;

    [Tooltip("The original colour to return this object to when deselected")]
    [SerializeField] private Color OriginalColor;

    // Start is called before the first frame update
    void Start()
    {
        OriginalColor = GetComponent<Renderer>().material.color;
    }

    /*
     * Assign a label to this object by the spawner
     */
    public void AddLabel(GameObject label)
    {
        GameObject frontLabel = Instantiate(label);
        frontLabel.transform.position = this.transform.position;
        frontLabel.transform.parent = this.transform;

        frontLabel.transform.Translate(new Vector3(0f, GetComponent<Collider>().bounds.extents.x + .01f, 0f));

        GameObject backLabel = Instantiate(label);
        backLabel.transform.position = this.transform.position;
        backLabel.transform.parent = this.transform;
        backLabel.transform.Translate(new Vector3(0f, -2f * (GetComponent<Collider>().bounds.extents.x), 0f));

    }

    /*
     * Returns the packaging enum as a string
     */
    public string MaterialString()
    {
        switch ((int)this.Package)
        {
            case 0: //trash
                {
                    return "Trash";
                    //break;
                }
            case 1: //plastic
                {

                    return "Plastic";
                    //break;
                }
            case 2: //non plastic recyclable
                {
                    return "CardBoard \nGlass";
                    //break;
                }
            default:
                return " ";
                //break;
        }
    }

    /**
     * Turns this item red when selected
     */
    public void OnSelected()
    {
        GetComponent<Renderer>().material.color = Color.red;
        Debug.Log("Selected");
    }

    /**
     * Turns this item back to its original colour when not deselected
     */
    public void OnDeselected()
    {
        Debug.Log("Deselected");
        GetComponent<Renderer>().material.color = OriginalColor;
    }

}
