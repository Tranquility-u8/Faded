using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBase : MonoBehaviour, IAttackable
{
    [SerializeField] protected bool isActivated = false;
    [SerializeField] private float damage = 5;

    private SpriteRenderer sprite;

    protected virtual void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    public void attack() {
        if (isActivated)
        {
            GameManager.instance.player.TakeDamage(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attack();
            AudioManager.instance.inSpike();
        }
    }

}
