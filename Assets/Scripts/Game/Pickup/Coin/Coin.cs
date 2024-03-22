using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dungeon;

public class Coin : Pickup
{

    [SerializeField] private CoinValue value;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        animator.Play("spin");
    }

    public override void OnPickUp(Player player)
    {
        player.getCoin( Constants.COIN_VALUES[(int)value]);
        //StartCoroutine(pickUpAnim());
        Destroy(gameObject);
    }

    IEnumerator pickUpAnim()
    {
        animator.Play("pickup");
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            OnPickUp(GameManager.instance.player);
        }
    }

}
