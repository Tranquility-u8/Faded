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
    public float damage;
    public float defense;

    public bool isVulnerable;
    public float vulnerableDuration;

    [Header("Animation")]
    public Animator animator;

    public abstract void Move();
    public abstract void TakeDamage(float damage);

}
