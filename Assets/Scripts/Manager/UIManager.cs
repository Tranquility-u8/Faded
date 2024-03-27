using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;
    private Player player;

    [Header("MiniMap")]
    [SerializeField] private GameObject miniMap;


    [Header("BottomBar")]
    [SerializeField] private GameObject bottomBar;
    [SerializeField] private Slider hpBar;
    [SerializeField] private Slider mpBar;
    [SerializeField] private Slider firstSlot;

    private bool isFirstUnfrozen = true;
    private float firstUnfrozenTime = 0;
    private float firstUnfrozenMaxTime = 5;

    [Header("TopBar")]
    [SerializeField] private TextMeshProUGUI coinText;

    [Header("Menu")]
    [SerializeField] private GameObject mainMenu;

    [Header("DeathBar")]
    [SerializeField] private GameObject deathBar;

    [Header("BossBar")]
    [SerializeField] private Slider bossHpBar;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        player = GameManager.instance.player;
    }

    void Start()
    {
        updateHpBar();
        updateMpBar();
        initFirstSlot();
    }

    void Update()
    {
        if (!mainMenu.activeSelf)
        {
            onMiniMap();
        }   
        onMenu();
        OnFirstSlot();
    }

    public void onMiniMap()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            miniMap.SetActive(!miniMap.activeSelf);
        }
    }

    public void updateHpBar()
    {
        float maxHealth = player.maxHealth;
        float currHealth = player.currHealth;
        hpBar.maxValue = maxHealth;
        hpBar.value = currHealth;
    }

    public void updateMpBar()
    {
        float maxMana = player.maxMana;
        float currMana = player.currMana;
        mpBar.maxValue = maxMana;
        mpBar.value = currMana;
    }

    public void updateCoinBar(int coinVal)
    {
        coinText.text = "x" + coinVal.ToString();
    }

    public void onMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            switchMenu();
        }

    }

    public void switchMenu()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        bottomBar.SetActive(!bottomBar.activeSelf);
        if (mainMenu.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void onDeathBar()
    {
        Time.timeScale = 0;
        deathBar.SetActive(true);
        bottomBar.SetActive(false);
    }

    //BossBar
    public void EnableBossHpBar(float maxh)
    {
        bossHpBar.maxValue = maxh;
        bossHpBar.value = maxh;
        bossHpBar.gameObject.SetActive(true);
    }

    public void updateBossHpBar(float currh)
    {
        bossHpBar.value = currh;
    }

    public void disableBossHpBar()
    {
        if(bossHpBar.gameObject != null)
           bossHpBar.gameObject.SetActive(false);
    }

    //BottomBar
    public void initFirstSlot()
    {
        firstSlot.value = firstSlot.maxValue = firstUnfrozenMaxTime;
    }

    public void OnFirstSlot()
    {
        if (!isFirstUnfrozen)
        {
            firstUnfrozenTime += Time.deltaTime;
            firstSlot.value = firstUnfrozenTime;
            if (firstUnfrozenTime >= firstUnfrozenMaxTime)
            {
                isFirstUnfrozen = true;
                firstUnfrozenTime = 0;
                firstSlot.value = firstUnfrozenMaxTime;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            player.OnPromptHpRecovery(10);
            isFirstUnfrozen = false;
            AudioManager.instance.inHeal();
        }
       
    }

    private void OnApplicationQuit()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
    }

}
