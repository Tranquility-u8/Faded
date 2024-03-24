using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuffBase : MonoBehaviour
{
    [SerializeField] protected int buffId = -1;

    public float timer = 0;
    [SerializeField] protected float duration = 0; //-1 means infinite

    [SerializeField] protected bool aboutVision = false;
    [SerializeField] protected bool aboutBattle = false;

    void Start()
    {
        GameManager.instance.player.buffPool.addBuff(this.GetType().Name);
    }

    protected void Timer()
    {
        timer += Time.fixedDeltaTime;
        if (timer > duration) { }
            OnTimeout();
    }

    protected void OnTimeout()
    {
        GameManager.instance.player.buffPool.removeBuff(this.GetType().Name);
        Destroy(gameObject);
    }

}
