using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/* A melee red monster */
public class GreenElite : MonsterBase
{

    [SerializeField] private NavMeshAgent agent;

    [Header("Melee")]
    [SerializeField] private float meleeRange;

    private bool isAttacking = false;
    private float disance = 20;

    [Header("Patrol")]
    [SerializeField] private Transform point1;
    [SerializeField] private Transform point2;

    [SerializeField] private bool isFirst;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;

        target = GameManager.instance.player.transform;
    }

    void Update()
    {
        if (!isAlive) return;

        disance = Vector3.Distance(transform.position, target.position);
        if (disance <= alertRadius)
        {
            isAlert = true;
        }
        else
        {
            isAlert = false;
        }
        animator.SetBool("isAlert", isAlert);

        if (disance <= meleeRange && !isAttacking)
        {
            isAttacking = true;
            StartCoroutine(TryAttack());
        }
    }

    private void FixedUpdate()
    {
        if (!isAlive) return;

        Move();
    }


    public override void Move()
    {
        if (isAttacking) return;
        
        if (isAlert)
        {
            agent.SetDestination(target.position);
        }
        else
        {
            if (isFirst)
            {
                agent.SetDestination(point1.position);
                if (Vector3.Distance(transform.position, point1.position) < agent.stoppingDistance)
                {
                    isFirst = false;
                }
            }
            else
            {
                agent.SetDestination(point2.position);
                if (Vector3.Distance(transform.position, point2.position) < agent.stoppingDistance)
                {
                    isFirst = true;
                }
            }
 
        }
        float dx = agent.steeringTarget.x - transform.position.x;
        int dir = dx > 0 ? 1 : -1;
        transform.localScale = new Vector3(dir, 1, 1);
    }


    public override void OnDead()
    {
        animator.Play("death");
        isAlive = false;
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
        else
        {
            animator.SetTrigger("OnHurt");
        }
    }

    IEnumerator TryAttack()
    {
        animator.SetTrigger("OnAttack");
        yield return new WaitForSeconds(0.75f);
        if (disance < meleeRange)
        {
            attack();
        }
        isAttacking = false;
    }

    public override void attack()
    {
        GameManager.instance.player.TakeDamage(actualDamage);
    }


}
