using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity: MonoBehaviour
{
    [Header("Life")]
    public float maxHealth;
    public float currHealth;
    public bool isAlive;

    [Header("Move")]
    public float speed;
    public Vector3 speedVec;
    public Vector2 position;

    [Header("Battle")]
    public float baseDamage;
    public float actualDamage;
    public float baseDefense;
    public float actualDefense;

    public bool isVulnerable;
    public float vulnerableDuration;

    [Header("Animation")]
    public Animator animator;

    [Header("SpriteRenderer")]
    public SpriteRenderer sprite;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    public abstract void Move();
    public abstract void TakeDamage(float damage);

}
