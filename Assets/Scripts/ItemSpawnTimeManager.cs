using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemSpawnTimeManager : MonoBehaviour
{
    [Header("Generic")]
    private float timeSinceLastSpawn = 0f;
    [SerializeField] float spawnInterval = 30f;
    [SerializeField] bool isAbleToSpawn = false; // If true, enemy has able to spawn item when they are dead
    
    [Header("Generic")]
    public List<GameObject> itemList;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAbleToSpawn){
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval)
            {
                isAbleToSpawn = true;
                timeSinceLastSpawn = 0f;
                Debug.Log("Item is able to be spawned, destroy enemy and get Items.");
            }
        }
    }

    public bool IsAbleToSpawnItem(){
        return isAbleToSpawn;
    }

    public GameObject SpawnItem(){
        return itemList[Random.Range(0, itemList.Count)];
    }
}
