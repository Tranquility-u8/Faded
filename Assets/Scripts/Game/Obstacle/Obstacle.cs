using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(NavMeshPlus.Components.NavMeshModifier))]
public abstract class Obstacle : MonoBehaviour, IDamageable, IDestrutable
{
    [SerializeField] protected Animator animator;

    [SerializeField] protected bool isDestroyed = false;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float currHealth;

    public abstract void OnDead();
    public abstract void OnDestoryed();
    public abstract void OnVulnerable();
    public abstract void TakeDamage(float damage);

}
