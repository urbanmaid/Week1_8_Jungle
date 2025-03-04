using UnityEngine;

public class Missile : MonoBehaviour
{
    public float damage;
    public SpriteRenderer rend;
    private Rigidbody2D theRb;
    public float speed = 3f;
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        theRb = GetComponent<Rigidbody2D>();
        theRb.linearVelocity = Vector2.right *speed;
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
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Kimminkyum0212_EnemyController enemy = collision.gameObject.GetComponent<Kimminkyum0212_EnemyController>();
            enemy.Damage(damage);
        }
    }
}
