using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(NavMeshPlus.Components.NavMeshModifier))]
public abstract class Obstacle : MonoBehaviour, IDamageable, IDestrutable
{
    protected Animator animator;
    protected SpriteRenderer sprite;
    
    [SerializeField] protected bool isDestroyed = false;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currHealth;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    protected virtual void Start()
    {
        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
    }

    public abstract void OnDead();
    public abstract void OnDestoryed();
    public abstract void OnVulnerable();
    public abstract void TakeDamage(float damage);

}
