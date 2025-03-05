using UnityEngine;

public class Kimminkyum0212_EnemyController : MonoBehaviour
{
    private Rigidbody2D enemyRb;
    protected GameObject player;
    [SerializeField] bool isShootable = true;

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

    [Header("Item")]
    [SerializeField] ItemSpawnTimeManager itemSpawnTimeManager;

    protected virtual void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        itemSpawnTimeManager = ItemSpawnTimeManager.instance;
    }

    void Update()
    {
        if (GameManager.instance.isPlaying)
        {
            //Enemy movement and rotation
            Vector2 moveDir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
            if (isShootable)
            {
                if (!inRange)
                {
                    enemyRb.linearVelocity = moveDir.normalized * moveSpeed;
                }
                else
                {
                    enemyRb.linearVelocity = Vector2.zero;
                    if (cooldown > 0)
                    {
                        cooldown -= Time.deltaTime;
                    }
                    else 
                    {
                        Shoot();
                        cooldown = fireRate;
                    }
                }
            } else {
                enemyRb.linearVelocity = moveDir.normalized * moveSpeed;
            }

        }
        else
        {
            enemyRb.linearVelocity = Vector2.zero;
        }

    }

    public void Damage(float dmgAmount)
    {
        health -= dmgAmount;
        if (health <= 0)
        {
            Destroy(gameObject);
            InstantiateItem(transform.position);
        }
    }

    private void InstantiateItem(Vector3 targetPosition)
    {
        if (itemSpawnTimeManager.IsAbleToSpawnItem())
        {
            Instantiate(itemSpawnTimeManager.SpawnItem(), targetPosition, Quaternion.identity);
        }
    }

    //shoots enemy projectile
    //used object pooling for projectile spawning and despawning
    private void Shoot()
    {
        GameObject projectile = Kimminkyum0212_ObjectPoolManager.instance.GetGo("Enemy Projectile");
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        projectile.GetComponent<Kimminkyum0212_Projectile>().Init(damage, projectileSpeed, scale, projectileColor);
    }
}
