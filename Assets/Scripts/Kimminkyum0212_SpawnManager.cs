using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class Kimminkyum0212_SpawnManager : MonoBehaviour
{
    public Transform player;
    public float spawnCoolDown;
    public Transform[] spawnPoints;
    public GameObject[] enemies;
    private Dictionary<int, int[]> stages;
    void Start()
    {
        stages = new Dictionary<int, int[]>();
        for (int i = 1; i <= Kimminkyum0212_GameManager.instance.stageCount; i++)
        {
            stages.Add(i, new int[5]);
        }
        stages[1] = new int[] { 10, 4, 0, 0, 0 };
        stages[2] = new int[] { 10, 1, 0, 0, 0 };
        stages[3] = new int[] { 10, 5, 0, 0, 0 };
        stages[4] = new int[] { 10, 10, 1, 0, 0 };
        stages[5] = new int[] { 0, 10, 5, 0, 0 };
        stages[6] = new int[] { 0, 10, 10, 1, 0 };
        stages[7] = new int[] { 0, 0, 10, 5, 0 };
        stages[8] = new int[] { 20, 10, 10, 0, 1 };

    }


    private void Update()
    {
        transform.position = player.position;
    }
    public int GetEnemyCount(int stageNum)
    {
        int[] enemyList = stages[stageNum];
        int enemyCount = 0;
        for (int i = 0; i < enemyList.Length; i++)
        {
            enemyCount += enemyList[i];
        }
        return enemyCount;
    }
    public void SpawnStage(int stageNum)
    {
        StartCoroutine(SpawnStageCoroutine(stageNum));
    }
    IEnumerator SpawnStageCoroutine(int stageNum)
    {
        int[] enemyList = stages[stageNum];
        for (int i = 0; i < enemyList.Length; i++)
        {
            for (int j = 0; j < enemyList[i]; j++)
            {
                yield return new WaitForSeconds(spawnCoolDown);
                spawnEnemy(enemies[i]);
            }
        }
    }

    private void spawnEnemy(GameObject enemyToSpawn)
    {
        Instantiate(enemyToSpawn, spawnPoints[Random.Range(0, spawnPoints.Length)].position, transform.rotation);
    }
}
