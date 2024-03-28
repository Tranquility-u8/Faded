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

    protected AudioSource audioSource;
    public MonsterType type;


    public abstract void attack();

    public void OnDead()
    {
        animator.Play("death");
        isAlive = false;
        var colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
        StartCoroutine(updatePool());
    }

    IEnumerator updatePool()
    {
        yield return new WaitForSeconds(2.0f);
        RoomBase room = DungeonManager.instance.currRoom;
        if(room.GetComponent<NormalRoom>() != null)
        {
            room.GetComponent<NormalRoom>().monsterPool.updatePool();
        }
        else if(room.GetComponent<BossRoom>() != null)
        {
            room.GetComponent<BossRoom>().monsterPool.updatePool();
        }

    }

    public abstract void OnVulnerable();

}
