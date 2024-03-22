using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/* A melee red monster */
public class RedElite : MonsterBase
{

    [SerializeField] protected NavMeshAgent agent;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GameManager.instance.player.transform;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= alertRadius)
            isAlert = true;
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
        if (isAlert) {
            agent.SetDestination(target.position);

            //animator.Play("pursue");
            float dx = agent.steeringTarget.x - transform.position.x;
            int dir = dx > 0 ? 1 : -1;
            transform.localScale = new Vector3(dir, 1, 1);
        }
        //animator.Play("Idle");

    }

    public override void OnDead()
    {
        throw new System.NotImplementedException();
    }

    public override void OnVulnerable()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float damage)
    {
        currHealth -= Mathf.Max((damage - defense), 0);
        if (currHealth < 0)
        {
            OnDead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.player.TakeDamage(damage);
        }
    }

}
