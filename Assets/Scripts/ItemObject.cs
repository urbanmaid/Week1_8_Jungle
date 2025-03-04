using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public int itemCode; 
    /* 
    Item Code: 
    0 for Heal
    1 for Missile
    */

    private float rotSpeed = 8f;
    private GameManager gm;

    [SerializeField] float healMount = 10f;
    [SerializeField] int missileAmount = 5;

    private void Start()
    {
        gm = GameManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Item has been picked up.");
            ItemEffect();
            Destroy(gameObject);
        }
    }

    public void ItemEffect()
    {
        switch (itemCode)
        {
            case 0:
                gm.DamagePlayer(-1 * healMount);
                break;
            case 1:
                //gm.ChargeMissileAmount();
                Debug.Log("Missile amount has been increased into " + missileAmount);
                break;
            default:
                Debug.LogError("Invalid item code detected, no effect applied.");
                break;
        }
    }
}
