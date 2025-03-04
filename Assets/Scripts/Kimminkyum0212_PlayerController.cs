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
            //플레이어가 마우스 커서로 가리키는 방향으로 회전]
            //플레이어 오브젝트를 회전시키면 플레이어 이동하는 방향이 바뀌기 때문에 shooter 오브젝트를 회전
            Vector3 mousePos = Input.mousePosition;
            Vector3 worldPos = mainCam.WorldToScreenPoint(transform.localPosition);
            Vector2 offset = new Vector2(mousePos.x - worldPos.x, mousePos.y - worldPos.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            shooter.transform.rotation = Quaternion.Euler(0, 0, angle);

            //WASD 인풋에 따라 이동
            //구조물에 맞아 튕기는 중이 아니라면 이동 가능
            if (!isBouncing)
            {
                Vector2 moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
                transform.Translate(moveDir.normalized * Mathf.Lerp(0, moveSpeed, 0.5f) * Time.deltaTime, Space.World);
            }

            //플레이어 사격
            //마우스 클릭 시 사격
            //사격속도에 제한이 있음
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
    //장애물과 충돌 시 플레이어에게 피해를 주고 췽겨나감
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

    //플레이어가 필드를 벗어날 시 일정 시간 후 패배
    //시간 내로 복귀하면 플레이 진행
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

    //플레이어가 필드를 벗어나면 10초간의 시간 후 게임 패배
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
