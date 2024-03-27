using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Skeleton : MonsterBase
{

    [SerializeField] private NavMeshAgent agent;

    [Header("Melee")]
    [SerializeField] private float meleeRange;
    
    private bool isAttacking = false;
    private float disance = 20;

    private AudioSource walkAudio;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();

        agent = GetComponent<NavMeshAgent>();
        var audioSources = GetComponents<AudioSource>();
        audioSource = audioSources[0];
        walkAudio = audioSources[1];

        audioSource.clip = AudioManager.instance.hitClip;
        walkAudio.clip = AudioManager.instance.skeletonWalkClip;
    }

    private void Start()
    {

        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        
        target = GameManager.instance.player.transform;

        walkAudio.Play();
    }

    void Update()
    {
        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);

        if (!isAlive) return;

        disance = Vector3.Distance(transform.position, target.position);
        if(disance <= alertRadius)
        {
            isAlert = true;
        }
        else
        {
            isAlert = false;
        }
        animator.SetBool("isAlert", isAlert);
    
        if(disance <= meleeRange && !isAttacking)
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
        if (isAlert && !isAttacking) {
            agent.SetDestination(target.position);
            float dx = agent.steeringTarget.x - transform.position.x;
            int dir = dx > 0 ? 1 : -1;
            transform.localScale = new Vector3(dir, 1, 1);

            walkAudio.Play();
        }
        //animator.Play("Idle");
    }

    public override void OnVulnerable()
    {
        throw new System.NotImplementedException();
    }

    public override void TakeDamage(float actualDamage)
    {
        currHealth -= Mathf.Max((actualDamage - actualDefense), 0);
        if (currHealth < 0 && isAlive)
        {
            walkAudio.Stop();
            OnDead();
           
        }
        else
        {
            animator.SetTrigger("OnHurt");
            audioSource.Play();
        }
    }

    IEnumerator TryAttack()
    {
        animator.SetTrigger("OnAttack");
        yield return new WaitForSeconds(0.75f);
        if(disance < meleeRange)
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
