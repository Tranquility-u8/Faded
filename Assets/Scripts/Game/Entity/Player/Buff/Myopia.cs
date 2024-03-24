using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Myopia : BuffBase
{
    public static Myopia instance;

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

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    private void OnApplicationQuit()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
    }
}
