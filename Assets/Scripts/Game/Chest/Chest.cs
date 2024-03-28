using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Chest : MonoBehaviour, IInteractive
{

    

    private bool isAround = false;
    private bool isOpen = false;

    private Animator animator;
    private SpriteRenderer sprite;

    [Header("Coin")]
    [SerializeField] private CoinPool coinPool;
    [SerializeField] private int maxValue;

    [Header("Item")]
    [SerializeField] private ItemBase item;

    [Header("Audio")]
    private AudioSource audioSource;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        animator.Play("idle");
        SetUpChest();

        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
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
        int totalVal = Random.Range(1, maxValue);
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
            audioSource.Play();
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
