using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/* A melee red monster */
public class RedSkull : MonsterBase
{

    [SerializeField] private bool isAround = false;
    [SerializeField] private float aroundRadius;

    private void Start()
    {
        isAlert = true;
        target = GameManager.instance.player.transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= aroundRadius)
            isAround = true;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Move()
    {
        if (isAlert)
        {
            animator.Play("pursue");
            if (isAround)
            {
                transform.RotateAround(target.transform.position, new Vector3(0, 0, 1), speed / aroundRadius);
            }
            else
            {
                Vector3.MoveTowards(transform.position, target.position, speed * Time.fixedDeltaTime);
            }
            
        }
        else
        {
            animator.Play("Idle");
        }
    }

    public override void OnVulnerable()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float actualDamage)
    {
        currHealth -= Mathf.Max((actualDamage - actualDefense), 0);
        if (currHealth < 0)
        {
            OnDead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.player.TakeDamage(baseDamage);
        }
    }

}
