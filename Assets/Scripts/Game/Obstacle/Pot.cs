using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : Obstacle
{
    [SerializeField] private CoinPool coinPool;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetUpPot();
    }


    public void SetUpPot()
    {
        currHealth = maxHealth;
        StartCoroutine(SetUpCoinPool());
    }

    IEnumerator SetUpCoinPool()
    {
        yield return null;
        int totalVal = Random.Range(0, 2);
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
