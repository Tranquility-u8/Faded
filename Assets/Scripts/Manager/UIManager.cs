using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


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
    
    [Header("Menu")]
    [SerializeField] public GameObject mainMenu;

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
    }

    void Update()
    {
        if (!mainMenu.activeSelf)
        {
            onMiniMap();
        }   
        onMenu();
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


    public void onMenu()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            mainMenu.SetActive(!mainMenu.activeSelf);
            bottomBar.SetActive(!bottomBar.activeSelf);
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
