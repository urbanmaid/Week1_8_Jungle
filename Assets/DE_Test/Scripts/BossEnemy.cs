using UnityEngine;

public class BossEnemy : EnemyController
{
    [Header("Boss Enemy")]
    public float bossMoveSpeed = 1;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    protected virtual void Update()
    {
        if (!GameManager.instance.isPlaying)
        {
            return;
        }

        if (!player)
        {
            return;
        }

        // Move toward player
        Vector2 targetDir = (player.transform.position - transform.position).normalized;
        var angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        transform.position =
            Vector2.MoveTowards(transform.position, player.transform.position, bossMoveSpeed * Time.deltaTime);
    }

    private void OnDestroy()
    {
        GameManager.instance.bossSpawnManager.enableBossSpawn();
        // PlayerInterfaceController.SetBossObject
    }
}
