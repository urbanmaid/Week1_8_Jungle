using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private GameManager gm;
    [SerializeField] TextMeshProUGUI healthText, missileText, scoreText, timerText;
    [SerializeField] TextMeshProUGUI moveLvlText, attackLvlText, skillLvlText;
    [SerializeField] Slider healthSlider;
    [SerializeField] TextMeshProUGUI scoreTextGameOver;

    [SerializeField] GameObject startPanel, upgradePanel, endPanel, gameInfo;
    [SerializeField] int moveLvl, attackLvl, skillLvl;
    private float time;
    private int min;
    private int sec;
    public int lvl;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        gm = GameManager.instance;
        timerText.text = "00:00";
        time = 0;
        min = 0;
        sec = 0;
    }

    void Update()
    {
        if (gm.isPlaying)
        {
            time += Time.deltaTime;
            UpdateTimer();
        }

    }

    // Update is called once per frame
    public void UpdateScore()
    {
        scoreText.text = gm.totalScore.ToString();
    }

    public void UpdateHealth()
    {
        healthSlider.value = gm.curHealth;
        healthText.text = gm.curHealth.ToString();
    }

    public void UpdateMissile()
    {
        missileText.text = "" + gm.missileAmount;
    }

    void UpdateTimer()
    {
        min = (int)(time / 60f);
        sec = (int)Mathf.Ceil(time % 60f) - 1;
        timerText.text = min + ":" + sec.ToString("D2");
    }

    public void StartGame()
    {
        startPanel.SetActive(false);
        gm.isPlaying = true;
        gm.player.SetActive(true);
        gm.managers.SetActive(true);
        gameInfo.SetActive(true);
    }

    public void Upgrade()
    {
        gm.isPlaying = false;
        upgradePanel.SetActive(true);
    }

    public void UpgradeChoice(int upgradeCode)
    {
        switch (upgradeCode)
        {
            case 0:
                //upgrade move speed;
                gm.player.GetComponent<PlayerController>().moveSpeed *= 1.5f;
                moveLvl += 1;
                moveLvlText.text = "Lv. " + moveLvl;
                break;
            case 1:
                //upgrade attack speed;
                gm.player.GetComponent<PlayerController>().fireRate *= 0.9f;
                attackLvl += 1;
                attackLvlText.text = "Lv. " + attackLvl;
                break;
            case 2:
                //upgrade skill power;
                gm.player.GetComponent<PlayerController>().skillPower *= 1.5f;
                skillLvlText.text = "Lv. " + skillLvl;
                break;
            default:
                Debug.Log("Wrong Upgrade Code");
                break;
        }
        gm.isPlaying = true;
        upgradePanel.SetActive(false);
    }

    public void EndGame()
    {
        gameInfo.SetActive(false);
        endPanel.SetActive(true);
        scoreTextGameOver.text = "" + gm.totalScore;
    }

    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ExitGame(){
        Application.Quit();
    }
}
