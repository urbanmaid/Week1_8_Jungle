using UnityEngine;

public class Kimminkyum0212_EnemyController : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    private GameObject player;

    [Header("Enemy Info")]
    public float health;
    public float moveSpeed;
    public bool inRange;

    
    [Header("Projectile Info")]
    public float damage;
    public float projectileSpeed;
    public float fireRate;
    private float cooldown;
    public float scale;
    public Color projectileColor;
    
    void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (Kimminkyum0212_GameManager.instance.isPlaying)
        {
            //Enemy movement and rotation
            Vector2 moveDir = player.transform.position - transform.position;
            if (!inRange)
            {
                enemyRb.linearVelocity = moveDir.normalized * moveSpeed;
            }
            else
            {
                enemyRb.linearVelocity = Vector2.zero;
            }

            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            if (cooldown > 0)
            {
                cooldown -= Time.deltaTime;
            }
            else if (inRange)
            {
                Shoot();
                cooldown = fireRate;
            }
        } else
        {
            enemyRb.linearVelocity = Vector2.zero;
        }
        
    }

    public void Damage(float dmgAmount)
    {
        health -= dmgAmount;
        if(health <= 0)
        {
            Kimminkyum0212_GameManager.instance.enemyCount--;
            Kimminkyum0212_GameManager.instance.UpdateEnemyCount();
            Destroy(gameObject);
        }
    }

    //shoots enemy projectile
    //used object pooling for projectile spawning and despawning
    private void Shoot()
    {
        GameObject projectile =  Kimminkyum0212_ObjectPoolManager.instance.GetGo("Enemy Projectile");
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        projectile.GetComponent<Kimminkyum0212_Projectile>().Init(damage, projectileSpeed, scale, projectileColor);
    }
}
