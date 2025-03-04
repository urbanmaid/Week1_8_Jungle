using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    [Header("Generic")]
    [SerializeField] List<GameObject> bossList;
    private GameObject player;
    internal Kimminkyum0212_PlayerController pc;
    private Kimminkyum0212_GameManager gm;
    public bool isBossSpawned = false;

    [Header("Timing Control")]
    private float timeSinceLastSpawn;
    [SerializeField] float spawnInterval = 10f;


    [Header("Dist Control")]
    [SerializeField] float distFromPlayerToEnemy = 30f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pc = player.GetComponent<Kimminkyum0212_PlayerController>();
        gm = GameObject.Find("Game Manager").GetComponent<Kimminkyum0212_GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(true && !isBossSpawned){
            timeSinceLastSpawn += Time.deltaTime;
            if (timeSinceLastSpawn >= spawnInterval)
            {
                Debug.Log("Boss spawned");
                SpawnBoss();
                isBossSpawned = true;
                timeSinceLastSpawn = 0f;
            }
        }
    }

    void SpawnBoss()
    {
        Instantiate(bossList[Random.Range(0, bossList.Count)], SetSpawnLocation(), Quaternion.identity);
    }

    private Vector3 SetSpawnLocation()
    {
        // Set new area of spawnning bullet box
        Vector3 playerPosition = player.transform.position;
        Vector2 randomDirection = Random.insideUnitCircle.normalized * distFromPlayerToEnemy;
        return playerPosition + new Vector3(randomDirection.x, randomDirection.y, 0);
    }

    internal void enableBossSpawn(){
        isBossSpawned = false;
    }
}
