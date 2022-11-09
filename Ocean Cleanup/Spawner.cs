using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**********************
 * @Brief Script : Spawner
 * @Brief Spawns objects for level 1
 * 
 * @Author : William Halling
 **********************/ 
public class Spawner : MonoBehaviour
{
    public int spawnOriginX;
    public int spawnOriginZ;
    public int spawnRangeX;
    public int spawnRangeZ;
    public float waterY;
    public int numSpawns;
    public List<GameObject> spawnableItems;


        /**********************
         * @Brief : Start()
         * @Brief : called before first update frame
         * 
         * @Author : William Halling
         **********************/

    void Start()
    {
        for (int i = 0; i < numSpawns; i++)
        {
            SpawnRandomObject();
            Debug.Log("Spawn #: " + i);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }


        /**********************
         * @Brief : RandomSpawnPosition()
         * @Brief : sets a random spawn position for an object
         * 
         **********************/

    private Vector3 RandomSpawnPosition()
    {
        return new Vector3(Random.Range(-spawnRangeX, spawnRangeX) + spawnOriginX, waterY, Random.Range(-spawnRangeZ, spawnRangeZ) + spawnOriginZ);
    }


        /**********************
         * @Brief : SpawnRandomObject()
         * @Brief : spawns an object within a random position on the map
         * 
         **********************/

    private void SpawnRandomObject()
    {
        if (spawnableItems.Count > 0)
        {
            Quaternion rotation = new Quaternion(0, 0, 0, 0);
            int index = Random.Range(0, spawnableItems.Count);
            Debug.Log("List index : " + index);
            var newspawn = Instantiate(spawnableItems[index], RandomSpawnPosition(), rotation);

            Debug.Log("New spawn items position: " + newspawn.transform.position);
            newspawn.SetActive(true);
        }
    }
}
