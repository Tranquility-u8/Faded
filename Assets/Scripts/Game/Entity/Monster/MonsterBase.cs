using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

using Dungeon;


public abstract class MonsterBase : Entity, IAttackable, IDamageable
{

    [SerializeField] protected Transform target;

    [SerializeField] protected bool isAlert = false;
    [SerializeField] protected float alertRadius;

    public MonsterType type;

    public abstract void attack();
    
    public abstract void OnDead();
    
    public abstract void OnVulnerable();

}
