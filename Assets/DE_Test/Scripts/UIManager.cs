using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    private GameManager gm;
    [SerializeField] TextMeshPro healthText, missileText, scoreText, timerText;
    [SerializeField] Slider healthSlider;

    [SerializeField] GameObject startPanel, upgradePanel, endPanel;
    private float time;
    private int min;
    private int sec;
    
    void Awake()
    {
        if(instance = null){
            instance = this;
        }
        
    }
    void Start()
    {
        gm = GameManager.instance;
        timerText.text = "0:00";
        time = 0;
        min = 0;
        sec = 0;
              
    }

    void Update()
    {
        time += Time.deltaTime;
        UpdateTimer();
    }

    // Update is called once per frame
    public void UpdateScore(float scorePoint){
        scoreText.text = "Score : " + 0;
    }

    public void UpdateHealth(){
        healthSlider.value = gm.curHealth;
    }

    public void UpdateMissile(){
        missileText.text = "" + gm.missileAmount;
    }

    void UpdateTimer(){
        min = (int)(time/60f);
        sec = (int)Mathf.Ceil(time % 60f);
        timerText.text = min + ":" + sec;
    }
}
