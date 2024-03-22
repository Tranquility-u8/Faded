using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuffBase : MonoBehaviour
{

    public float duration = 0; //-1 means infinite
    public float timer = 0;

    public bool aboutEffect = false;
    public bool aboutBattle = false;

    protected void Timer()
    {
        timer += Time.fixedDeltaTime;
        if (timer > duration)
            OnTimeout();
    }

    protected void OnTimeout()
    {

    }

}
