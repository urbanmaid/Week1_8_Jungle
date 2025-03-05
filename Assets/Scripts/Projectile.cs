using UnityEngine;

public class Projectile : MonoBehaviour
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

    // void Update()
    // {
    //     if(!rend.isVisible){
    //         Destroy(gameObject);
    //     }
    // }
    void OnBecameInvisible()
    {
        Debug.Log("Not Visible");
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player Projectile") && collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.Damage(damage);
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Enemy Projectile") && collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.DamagePlayer(damage);
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Missile Projectile") && collision.gameObject.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();
            enemy.Damage(damage);
        }
    }
}
