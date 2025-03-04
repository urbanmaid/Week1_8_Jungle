using UnityEngine;

public class Projectile : Kimminkyum0212_Poolable
{
    private Rigidbody2D theRb;
    public float projectileSpeed;
    private SpriteRenderer rend;
    public float damage;

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        theRb = GetComponent<Rigidbody2D>();
        theRb.linearVelocity = transform.right * projectileSpeed;
    }

    private void Update()
    {
        if (!rend.isVisible)
        {
           Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player Projectile") && collision.gameObject.CompareTag("Enemy"))
        {
            Kimminkyum0212_EnemyController enemy = collision.gameObject.GetComponent<Kimminkyum0212_EnemyController>();
            enemy.Damage(damage);
            Destroy(gameObject);
        } else if(gameObject.CompareTag("Enemy Projectile") && collision.gameObject.CompareTag("Player"))
        {
            Kimminkyum0212_GameManager.instance.DamagePlayer(damage);
            Destroy(gameObject);
        }
    }
}
