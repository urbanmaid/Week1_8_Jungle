using UnityEngine;

public class Kimminkyum0212_Projectile : Kimminkyum0212_Poolable
{
    private Rigidbody2D theRb;
    public float projectileSpeed;
    private SpriteRenderer rend;
    public float damage;

    public void Init(float newDam, float newSpeed, float scale, Color color)
    {
        rend = GetComponent<SpriteRenderer>();
        theRb = GetComponent<Rigidbody2D>();
        transform.localScale = new Vector2(scale, scale);
        rend.color = color;
        damage = newDam;
        projectileSpeed = newSpeed;
        theRb.linearVelocity = transform.right * projectileSpeed;
    }

    private void Update()
    {
        if (!rend.isVisible)
        {
            Pool.Release(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player Projectile") && collision.gameObject.CompareTag("Enemy"))
        {
            Kimminkyum0212_EnemyController enemy = collision.gameObject.GetComponent<Kimminkyum0212_EnemyController>();
            enemy.Damage(damage);
            ReleaseObject();
        } 
        else if(gameObject.CompareTag("Enemy Projectile") && collision.gameObject.CompareTag("Player"))
        {
            Kimminkyum0212_GameManager.instance.DamagePlayer(damage);
            ReleaseObject();
        }
    }
}
