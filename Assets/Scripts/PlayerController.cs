using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Player Movement")]
    private Rigidbody2D playerRb;
    public float moveSpeed;
    private Camera mainCam;
    public GameObject shooter;

    [Header("Projectile")]
    public GameObject projectile;
    public float fireRate;
    private float coolDown;
    public float damage = 5;
    public float projectileSpeed = 7;
    public Color projectileColor;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        coolDown = 0;
        playerRb = gameObject.GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 worldPos = mainCam.WorldToScreenPoint(transform.localPosition);
        Vector2 offset = new Vector2(mousePos.x - worldPos.x, mousePos.y - worldPos.y);
        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        shooter.transform.rotation = Quaternion.Euler(0, 0, angle);

        
        
        
        Vector2 moveDir = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(moveDir.normalized * Mathf.Lerp(0, moveSpeed, 0.5f) * Time.deltaTime, Space.World);
        if (coolDown > 0)
        {
            coolDown -= Time.deltaTime;
        } else 
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();

            }
        }
    }

    void Shoot()
    {
        //GameObject projectile = Kimminkyum0212_ObjectPoolManager.instance.GetGo("Player Projectile");
        projectile.transform.position = transform.position;
        projectile.transform.rotation = shooter.transform.rotation;
        projectile.GetComponent<Kimminkyum0212_Projectile>().Init(damage, moveSpeed, 0.1f, projectileColor);
        coolDown = fireRate;
    }
}
