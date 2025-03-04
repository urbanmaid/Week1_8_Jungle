using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BossEnemy : MonoBehaviour
{
    protected GameObject Player;
    public float moveSpeed = 1;
    public int collisionDamage = 1;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual void Update()
    {
        if (Player == null)
        {
            return;
        }

        // Move toward player
        Vector2 targetDir = (Player.transform.position - transform.position).normalized;
        var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.position =
            Vector2.MoveTowards(transform.position, Player.transform.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("@@DE ---> 적과 충돌!");
            if (GameManager.instance != null)
            {
                GameManager.instance.DamagePlayer(collisionDamage);
            }
        }
    }
    
    private void DespawnBossEnemy()
    {
        // Check isAbleToSpawn
        // Spawn Item
    }
}
