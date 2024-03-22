using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chest : MonoBehaviour, IInteractive
{

    [SerializeField] private bool isAround = false;
    [SerializeField] private bool isOpen = false;

    [SerializeField] private Animator animator;

    [SerializeField] private CoinPool coinPool;
    [SerializeField] private ItemBase item;
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        animator.Play("idle");
        SetUpChest();
    }

    void Update()
    {
        if(!isOpen)
            OnInteract();    
    }

    public void SetUpChest()
    {
        StartCoroutine(SetUpCoinPool());
        StartCoroutine(SetUpItem());
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

    IEnumerator SetUpItem()
    {
        yield return null;
        int totalVal = Random.Range(0, Constants.itemDictionary.Count);
    }

    public void OnInteract()
    {
        if (isAround && Input.GetKeyDown(Constants.OPEN_CHEST))
        {
            coinPool.OnRelease();
            animator.Play("open");
            isOpen = true;
            //Debug.Log("open the chest");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isAround = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isAround = false;
    }

}
