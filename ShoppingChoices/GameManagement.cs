using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


/****************
 * @class GameManagementx
 * @brief This class handles generating the shopping list and checking if the list has all been
 * completed to start the game ending
 *
 * @author William Halling
 * @version 01
 * @date 18/05/2022
 *
 ***************/


public class GameManagement : MonoBehaviour
{

    [Tooltip("Copy of Checkout Items are added tot his list")]
    public List<GameObject> CollectedItems;


    [Tooltip("Count of collected items to check if all items collected")]
    [SerializeField] private int CollectedCount = 0;


    [Tooltip("Drop Shelves spawning items into this list to make available\n for the shopping list")]
    public List<GameObject> ItemPool;


    [Tooltip("How many items to randomly add to the shopping list for difficulty")]
    public int CheckListSize = 0;


    [Tooltip("String names of the items on the shopping list")]
    public List<string> CheckList;


    [Tooltip("Drop the textboxes for the shopping list here")]
    public List<GameObject> CheckListTextBoxes;


    [Tooltip("Game running time")]
    public GameObject Timer;


    [Tooltip("This event called when all items are collected")]
    public UnityEvent onAllItemsCollected;


    void Awake()
    {
        Timer = Timer ?? GameObject.Find("GameTime");
    }


        /*******************
         * @Brief : Start()
         * @Brief : Generates a new list of items at the start of the level
         * 
         *******************/ 

    void Start()
    {
        CollectedItems = new List<GameObject>();
        CreateShoppingList();
    }

        /*********************************
         * @Brief : ItemDroppedOff()
         * @Brief : Manages list and Checks if items are dropped in the drop zone
         *********************************/
    
    public void ItemDroppedOff(GameObject item)
    {
        CollectedItems.Add(item);
        CollectedCount++;

        //lines through name
        RemoveOfffListItem(item.GetComponentInChildren<ShoppingItem>().ItemName);

        if (CollectedCount == CheckListSize)
        {
            //game over
            Timer.SetActive(false);
            this.onAllItemsCollected.Invoke();
        }
    }


        /******************************************************
         * @Brief : CreateShoppingList()
         * @Brief : Createas a shopping list for the player to collect
         ******************************************************/

    public void CreateShoppingList()
    {
        if (ItemPool.Count < CheckListSize)
        {
            Debug.Log("Insufficient Pool items for set checklist size");

            CheckListSize = ItemPool.Count;
        }


        if (CheckListTextBoxes.Count < CheckListSize)
        {
            Debug.Log("Insufficient Checklist text boxes to display all items");

            CheckListSize = CheckListTextBoxes.Count;
        }

        
        while (CheckList.Count < CheckListSize)
        {
            var random = new System.Random();
            int index = random.Next(ItemPool.Count);
            //check index doesnt exist and no duplicates
            string item_Name = ItemPool[index].GetComponent<SpawnItems>().ShelfName;


            if (!CheckList.Contains(item_Name))
            {
                CheckList.Add(item_Name);
            }
        }

        //set the checklist in the UI
        int count = 0;

        foreach(GameObject textBox in CheckListTextBoxes)
        {
            if (count < CheckList.Count)
            {
                textBox.GetComponent<Text>().text = CheckList[count];
            }
            else
            {
                textBox.GetComponent<Text>().text = " ";
            }
            count++;
        }

    }

        /******
         * @Brief : RemoveOffListItem()
         * @Brief : Removes this string name off the check out list if it is on it
         ******/

    void RemoveOfffListItem(string item_Name)
    {
        foreach(GameObject textBox in CheckListTextBoxes)
        {
            if(textBox.GetComponent<Text>().text == item_Name)
            {
                textBox.GetComponent<Text>().color = Color.grey;
            }
        }
    }
}
