using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using TMPro;


/**
 * @class SpawnItems
 * @brief This class is responsible for randomising the labelling and packaging of its child Shopping items
 *
 * @author Michael John
 * @version 01
 * @date 18/05/2022
 *
 */
public class SpawnItems : MonoBehaviour
{
    [Tooltip("Shopping items to have randomised packaging on start")]
    public List<GameObject> ShelfItems;

    [Tooltip("The indices of which randomly selected items are a good choice/ environmentally friendly")]
    public List<int> EnviroFriendlyIndices;

    [Tooltip("The Label to be applied for non plastic/non trash items(GOOD items)")]
    public Sprite RecycleLabel;

    [Tooltip("The Label to be applied for non plastic/ bad items")]
    public Sprite PlasticLabel;
    [Tooltip("The Label to be applied for trash/ bad items")]
    public Sprite TrashLabel;

    [Tooltip("The item name to be applied for it the children shopping items")]
    public string ShelfName;

    [Tooltip("Drop all text signage here to display this shelves item names")]
    public List<TMP_Text> Signage;

    [Tooltip("Number of items to be made spawned as environmentally friendly")]
    [Min(1)]
    public int NumEnviroFriendly;

    // Start is called before the first frame update
    void Start()
    {
        //if items have been assigned to this shelf
        if(ShelfItems != null)
        {
            //cant have more enviro friendly items than assigned on shelf
            if(NumEnviroFriendly < ShelfItems.Count && ShelfItems.Count > 1)
            {
                EnviroFriendlyIndices = new List<int>();
                randomizeEnviroFriendlyIndices();
                placeLabels();
                foreach (TMP_Text Sign in Signage)
                {
                    Sign.text = ShelfName;
                }
            }
            else
            {
                Debug.Log("not enough children shopping items attached for number of enviro friendly to spawn");
            }

        }
        else
        {
            Debug.Log("No items assigned to shelf");
        }
    }

    /**
     * Assigns items as environmentally friendly up to the specified variable amount
     */
    public void randomizeEnviroFriendlyIndices()
    {
        var random = new System.Random();
        int randomInt = 0;
        for (int i = 0; i < NumEnviroFriendly; i++)
        {
            randomInt = random.Next(ShelfItems.Count); 
            //want to find unique indices
            while (EnviroFriendlyIndices.Contains(randomInt))
            {

                randomInt = random.Next(ShelfItems.Count); 
            }
            EnviroFriendlyIndices.Add(randomInt);
        }
    }

    /**
     * Places the associated labels with what type of packaging they have been randomly assigned
     */
    public void placeLabels()
    {
        int index = 0;

        var random = new System.Random();
        //bool posOnAxis = true;
        foreach (GameObject Item in ShelfItems)
        {
            //get the ShoppingItem script here
            var ShoppingScript = Item.GetComponent<ShoppingItem>();

            //create a new label depending on quote of envirofriendly items to be spawned
            Sprite newLabel;
            //set enviro firendly or not
            if (EnviroFriendlyIndices.Contains(index))
            {
                newLabel = Instantiate(RecycleLabel);

                //get the script prior and then  work on it
                ShoppingScript.Package = Packaging.NonPlasticRecycle;
            }
            else
            {

                int randomInt = random.Next(0, 2); //either Plastic or trash
                Debug.Log("Shelf " + ShelfName + " random int " + randomInt);
                if (randomInt == 1)
                {
                    ShoppingScript.Package = Packaging.Plastic;
                    newLabel = Instantiate(PlasticLabel);
                }
                else
                {
                    ShoppingScript.Package = Packaging.Trash;
                    newLabel = Instantiate(TrashLabel);
                }
            }

            Item.GetComponentInChildren<SpriteMask>().sprite = newLabel;
            //set this to the prefab script for future use
            ShoppingScript.Label = newLabel;
            ShoppingScript.ItemName = this.ShelfName;
            index++;
        }
    }


}
