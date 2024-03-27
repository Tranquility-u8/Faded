using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class Bullet : MonoBehaviour
{
    private Player player;
    private BulletPool bulletPool;

    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private float damage;

    private AudioSource audioSource;

    void Awake()
    {
        player = GameManager.instance.player;
        bulletPool = player.bulletPool;

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        damage = player.baseDamage;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Bullet")) return;

        if (collision.gameObject.CompareTag("Monster"))
        {
            collision.gameObject.GetComponent<MonsterBase>().TakeDamage(damage);
            if (!player.isPenetrate)
            {
                //Debug.Log(collision.gameObject);
                //Debug.Log("Hit monster");
                OnHit();
                //audioSource.Play();
            }
        }     
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            collision.gameObject.GetComponent<Obstacle>()?.TakeDamage(damage);
            if (!player.isPenetrate)
            {
                //Debug.Log(collision.gameObject);
                //Debug.Log("Hit obstacle");
                OnHit();
            }
        }
        else if (collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Door"))
        {
            //Debug.Log(collision.gameObject);
            //Debug.Log("Hit Wall or Door");
            OnHit();
        }

    }

    private void OnHit()
    {
        rb.velocity = Vector2.zero;
        transform.position = Vector2.zero;
        if(bulletPool != null)
            bulletPool.Back(gameObject);
    }

}
