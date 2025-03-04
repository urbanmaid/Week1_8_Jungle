using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Kimminkyum0212_GameManager : MonoBehaviour
{
    public static Kimminkyum0212_GameManager instance;
    public Kimminkyum0212_SpawnManager spawnManager;
    [Header("Game Mechanic")]
    public GameObject player;
    public float maxHealth;
    private float curHealth;
    private int curStage;
    public int stageCount;
    public int enemyCount;
    public bool isPlaying;
    private bool paused;

    [Header("UI Elements")]
    public Slider healthSlider;
    public TextMeshProUGUI enemyCountText;
    public GameObject titleScreen;
    public GameObject playScreen;
    public GameObject endStage;
    public GameObject winScreen;
    public GameObject gameoverScreen;
    public GameObject upgradeButtons;
    public GameObject warningScreen;
    public GameObject pauseScreen;
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

    void Start()
    {
        paused = false;
        curStage = 1;
        titleScreen.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!paused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
            
        }
    }

    void UpdateSlider(float health)
    {
        healthSlider.value = health;
    }

    public void DamagePlayer(float damage)
    {
        curHealth -= damage;
        UpdateSlider(curHealth);
        if (curHealth <= 0)
        {
            isPlaying = false;
            player.SetActive(false);
            playScreen.SetActive(false);
            gameoverScreen.SetActive(true);
        }
        else if(curHealth > maxHealth)
        {
            Debug.Log("Health Overflow detected, resetting health to max.");
            curHealth = maxHealth;
            UpdateSlider(curHealth);
        }
    }

    public void StartGame()
    {
        player.SetActive(true);
        titleScreen.SetActive(false);
        playScreen.SetActive(true);
        StartStage();
    }

    public void StartStage()
    {
        endStage.SetActive(false);
        isPlaying = true;
        curHealth = maxHealth;
        UpdateSlider(curHealth);
        enemyCount = spawnManager.GetEnemyCount(curStage);
        UpdateEnemyCount();
        spawnManager.SpawnStage(curStage);
    }

    public void UpdateEnemyCount()
    {
        enemyCountText.text = "Enemy: " + enemyCount;
        if (enemyCount == 0)
        {
            EndStage();
        }
    }

    void EndStage()
    {
        isPlaying = false;
        curStage += 1;
        if (curStage <= stageCount)
        {
            endStage.SetActive(true);
            upgradeButtons.SetActive(true);
        }
        else
        {
            playScreen.SetActive(false);
            winScreen.SetActive(true);
        }

    }

    public void Restart()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void Upgrade(int type)
    {
        switch (type)
        {
            case 0:
                player.GetComponent<Kimminkyum0212_PlayerController>().moveSpeed += 2;
                break;
            case 1:
                player.GetComponent<Kimminkyum0212_PlayerController>().fireRate *= 0.75f;
                break;
            case 2:
                player.GetComponent<Kimminkyum0212_PlayerController>().damage += 5;
                break;
        }
        upgradeButtons.SetActive(false);
    }


    public void Pause()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        paused = true;
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        paused = false;
    }
    public void Quit()
    {
        Application.Quit();
    }

}
