using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    private List<GameObject> coins;

    [Header("Coin Prefabs")]
    public GameObject copperCoinPrefab;
    public GameObject silverCoinPrefab;
    public GameObject goldCoinPrefab;

    void Start()
    {
        coins = new List<GameObject>() { };
    }

    public void SetUpPool(int c, int s, int g)
    {
        GameObject coin;
        for(int i = 0; i < c; i++)
        {
            coin = Instantiate(copperCoinPrefab, this.transform.position, Quaternion.identity);
            coin.SetActive(false);
            coins.Add(coin);           
        }
        for(int i = 0; i < s; i++)
        {
            coin = Instantiate(silverCoinPrefab, this.transform.position, Quaternion.identity);
            coin.SetActive(false);
            coins.Add(coin);
        }
        for(int i = 0; i < g; i++)
        {
            coin = Instantiate(goldCoinPrefab, this.transform.position, Quaternion.identity);
            coin.SetActive(false);
            coins.Add(coin);
        }
    }

    public void OnRelease()
    {
        foreach(var coin in coins)
        {
            float vx = Random.Range(-1f, 1f) * 50;
            float vy = Random.Range(-1f, 1f) * 50;
            coin.SetActive(true);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(vx, vy));
        }
    }

}
