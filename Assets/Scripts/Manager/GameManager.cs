using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dungeon;


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Player player;

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
    }

    void Start()
    {
        Screen.SetResolution(1920, 1080, true);
    }

    void Update()
    {

    }


    private void OnApplicationQuit()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    public void Exit()
    {
        Application.Quit();
    }
}
