using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ShoppingList : MonoBehaviour
{
    public List<GameObject> ItemPool;
    public int CheckListSize = 0;
    public List<GameObject> CheckList;

    // Start is called before the first frame update
    void Start()
    {
        List<GameObject> ShoppingList = new List<GameObject>();
        CreateCheckList();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateCheckList()
    {
        if(ItemPool.Count < CheckListSize)
        {
            Debug.Log("Insufficient Pool items for set checklist size");
        }
        while(CheckList.Count < CheckListSize)
        {
            var random = new System.Random();
            int index = random.Next(ItemPool.Count);
            CheckList.Add(ItemPool[index]);
        }
    }
}
