using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameManager gm;
    private Rigidbody2D enemyRb;
    protected GameObject player;
    [SerializeField] bool isShootable = true;

    [Header("Enemy Info")]
    public float health;
    public float moveSpeed;
    public bool inRange;

    [SerializeField] protected float collisionDamage;
    [SerializeField] GameObject projectile;
    private float cooldown;
    public float fireRate;
    private bool canDamage;

    // [Header("Projectile Info")]
    // public float damage;
    // public float projectileSpeed;
    
    
    // public float scale;
    // public Color projectileColor;

    [Header("Item")]
    [SerializeField] ItemSpawnTimeManager itemSpawnTimeManager;

    protected virtual void Start()
    {
        enemyRb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        itemSpawnTimeManager = ItemSpawnTimeManager.instance;
        gm = GameManager.instance;
        canDamage = true;
    }

    void Update()
    {
        if (gm.isPlaying)
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
            // Spawn one instance of item and disable the ability to spawn item
            Instantiate(itemSpawnTimeManager.SpawnItem(), targetPosition, Quaternion.identity);
            itemSpawnTimeManager.SetAbleToSpawnItem(false);
        }
    }

    //shoots enemy projectile
    //used object pooling for projectile spawning and despawning
    private void Shoot()
    {
        Instantiate(projectile,transform.position, transform.rotation);        
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && canDamage){
            canDamage = false;
            gm.DamagePlayer(collisionDamage);
            StartCoroutine("WaitCo");
        }
    }

    IEnumerator WaitCo(){
        yield return new WaitForSeconds(1f);
        canDamage = true;
    }
}

