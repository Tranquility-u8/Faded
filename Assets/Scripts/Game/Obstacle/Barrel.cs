using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : Obstacle
{
    [SerializeField] private CoinPool coinPool;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
        SetUpBarrel();
    }


    public void SetUpBarrel()
    {
        currHealth = maxHealth;
        StartCoroutine(SetUpCoinPool());
    }

    IEnumerator SetUpCoinPool()
    {
        yield return null;
        int totalVal = Random.Range(1, 20);
        int gold = totalVal / Constants.COIN_VALUES[2];
        totalVal %= Constants.COIN_VALUES[2];
        int silver = totalVal / Constants.COIN_VALUES[1];
        totalVal %= Constants.COIN_VALUES[1];
        int copper = totalVal;
        coinPool.SetUpPool(copper, silver, gold);
    }

    public override void OnDead()
    {
        throw new System.NotImplementedException();
    }

    public override void OnDestoryed()
    {
        isDestroyed = true;
        animator.Play("destroy");
        coinPool.OnRelease();
    }

    public override void OnVulnerable()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float damage)
    {
        currHealth -= damage;
        if (!isDestroyed && currHealth <= 0)
            OnDestoryed();
    }

}
