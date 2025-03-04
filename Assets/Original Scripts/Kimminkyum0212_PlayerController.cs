using UnityEngine;
using System.Collections;
public class Kimminkyum0212_PlayerController : MonoBehaviour
{
    [Header("Player Movement")]
    private Rigidbody2D playerRb;
    public float moveSpeed;
    private Camera mainCam;
    public GameObject shooter;
    private bool isBouncing;
    public float bounceForce;

    [Header("Game Mechanic")]
    private bool inBound;

    [Header("Projectile")]
    //public GameObject projectile;
    public float fireRate;
    private float coolDown;
    public float damage = 5;
    public float projectileSpeed = 7;
    public Color projectileColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coolDown = 0;
        inBound = true;
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        isBouncing = false;
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Kimminkyum0212_GameManager.instance.isPlaying)
        {
            //�÷��̾ ���콺 Ŀ���� ����Ű�� �������� ȸ��]
            //�÷��̾� ������Ʈ�� ȸ����Ű�� �÷��̾� �̵��ϴ� ������ �ٲ�� ������ shooter ������Ʈ�� ȸ��
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = mainCam.WorldToScreenPoint(transform.localPosition);
            Vector2 offset = new Vector2(mousePos.x - worldPos.x, mousePos.y - worldPos.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            shooter.transform.rotation = Quaternion.Euler(0, 0, angle);

            //WASD ��ǲ�� ���� �̵�
            //�������� �¾� ƨ��� ���� �ƴ϶�� �̵� ����
            if (!isBouncing)
            {
                Vector2 moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                transform.Translate(moveDir.normalized * Mathf.Lerp(0, moveSpeed, 0.5f) * Time.deltaTime, Space.World);
            }

            //�÷��̾� ���
            //���콺 Ŭ�� �� ���
            //��ݼӵ��� ������ ����
            if (coolDown > 0)
            {
                coolDown -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButton(0))
                {
                    Shoot();

                }
            }
        }
    }
    //��ֹ��� �浹 �� �÷��̾�� ���ظ� �ְ� ��ܳ���
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Kimminkyum0212_GameManager.instance.DamagePlayer(5);
            Vector2 pushDir = transform.position - collision.transform.position;
            playerRb.AddForce(pushDir.normalized * bounceForce);
            isBouncing = true;
            Invoke("StopBounce", 0.3f);
        }
    }

    private void StopBounce()
    {
        playerRb.linearVelocity = Vector2.zero;
        isBouncing = false;

    }

    //�÷��̾ �ʵ带 ��� �� ���� �ð� �� �й�
    //�ð� ���� �����ϸ� �÷��� ����
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Limit"))
        {
            Kimminkyum0212_GameManager.instance.warningScreen.SetActive(true);
            inBound = false;
            StartCoroutine(OutofBounds());
        }
        else if (collision.gameObject.CompareTag("Enemy Range"))
        {
            Kimminkyum0212_EnemyController enemy = collision.gameObject.GetComponentInParent<Kimminkyum0212_EnemyController>();
            enemy.inRange = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Limit"))
        {
            Kimminkyum0212_GameManager.instance.warningScreen.SetActive(false);
            inBound = true;
        }
        else if (collision.gameObject.CompareTag("Enemy Range"))
        {
            Kimminkyum0212_EnemyController enemy = collision.gameObject.GetComponentInParent<Kimminkyum0212_EnemyController>();
            enemy.inRange = true;
        }
    }

    //�÷��̾ �ʵ带 ����� 10�ʰ��� �ð� �� ���� �й�
    IEnumerator OutofBounds()
    {
        yield return new WaitForSeconds(10);
        if (!inBound)
        {
            Kimminkyum0212_GameManager.instance.DamagePlayer(1000);
        }
    }

    void Shoot()
    {
        GameObject projectile = Kimminkyum0212_ObjectPoolManager.instance.GetGo("Player Projectile");
        projectile.transform.position = transform.position;
        projectile.transform.rotation = shooter.transform.rotation;
        projectile.GetComponent<Kimminkyum0212_Projectile>().Init(damage, moveSpeed, 0.1f, projectileColor);
        coolDown = fireRate;
    }
}
