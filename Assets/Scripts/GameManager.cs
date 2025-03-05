using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public RandomEnemySpawner spawnManager;
    
    [Header("Game Mechanic")]
    public GameObject player;
    public float maxHealth;
    private float curHealth;
    public bool isPlaying;
    public int missileAmount;
    private bool paused;

    [SerializeField] private int totalScore = 0;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curHealth = maxHealth;
        missileAmount = 0;

        StartCoroutine(IncreaseScore());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DamagePlayer(float damage)
    {
        curHealth -= damage;
        // UpdateSlider(curHealth);
        if (curHealth <= 0)
        {
            isPlaying = false;
            player.SetActive(false);
            // playScreen.SetActive(false);
            // gameoverScreen.SetActive(true);
        }
        else if(curHealth > maxHealth)
        {
            Debug.Log("Health Overflow detected, resetting health to max.");
            curHealth = maxHealth;
            // UpdateSlider(curHealth);
        }
    }

    private IEnumerator IncreaseScore()
    {
        while (isPlaying)
        {
            totalScore++;
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void IncreaseScore(int score)
    {
        totalScore += score;
    }
}
