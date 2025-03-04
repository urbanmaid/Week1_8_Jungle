using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [Header("Player Movement")]
    private Rigidbody2D playerRb;
    public float moveSpeed;
    private Camera mainCam;
    public GameObject shooter;

    [Header("Basic Projectile")]
    public GameObject projectile;
    public float fireRate;
    private float coolDown;
    public float damage = 5;
    public float projectileSpeed = 7;
    public Color projectileColor;

    [Header("Missile")]
    public GameObject missilePrefab;
    public float spawnDistance;
    private GameObject _currentProjectile;
    private float rotationSpeed = 100f;
    private float _rotateAngle = 90f;
    private int _rotateDirection = 0;
    public float launchSpeed = 5f;

    [Header("Skills")]
    public float dashTime = 1.5f;
    public GameObject gravityShot;

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
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                Shoot();

            }
        }

        if (Input.GetMouseButton(1))
        {
            if (_currentProjectile != null)
            {
                RotateProjectile();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            _rotateDirection = -1;
            SpawnProjectile();
        }

        if (Input.GetMouseButtonUp(1))
        {
            if (_currentProjectile != null)
            {
                // Launch current projectile
                LaunchProjectile();

            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GravityShot();
        }
    }

    void Shoot()
    {
        Instantiate(projectile, transform.position, shooter.transform.rotation);
        coolDown = fireRate;
    }

    private void SpawnProjectile()
    {
        if (_currentProjectile == null)
        {
            Vector3 spawnPos = transform.position + Vector3.up * spawnDistance;

            _currentProjectile = Instantiate(missilePrefab, spawnPos, Quaternion.identity);
            _currentProjectile.tag = "Untagged";
        }
    }

    private void RotateProjectile()
    {
        _rotateAngle += rotationSpeed * _rotateDirection * Time.deltaTime;
        var radian = _rotateAngle * Mathf.Deg2Rad;
        var newPos = transform.position + new Vector3(Mathf.Cos(radian), Mathf.Sin(radian), 0) * spawnDistance;
        _currentProjectile.transform.position = newPos;
    }

    private void LaunchProjectile()
    {
        // _currentProjectile.tag = "Projectile";

        var direction = (_currentProjectile.transform.position - transform.position).normalized;
        var rb = _currentProjectile.GetComponent<Rigidbody2D>();

        rb.linearVelocity = direction * launchSpeed;


        // Init current projectile
        _currentProjectile = null;
        _rotateAngle = 90;
        _rotateDirection = 0;
    }

    void GravityShot()
    {
        GameObject gShot = Instantiate(gravityShot, transform.position, shooter.transform.rotation);
        

    }
}
