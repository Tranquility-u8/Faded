using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dungeon;
using AllIn1SpriteShader;


// 记得把Player改成PlayerInBattle，避免数据冗余
public class Player : Entity, IAttackable, IDamageable
{
    Rigidbody2D rb;

    [Header("Magic")]
    public float maxMana;
    public float currMana;

    [Header("Bullet")]
    public BulletPool bulletPool;
    public float knockback;
    public float bulletSpeed;
    public float range;
    public bool isPenetrate;

    [Header("ItemBase")]
    public List<ItemBase> itemPool;

    [Header("Coin")]
    public int coins = 0;

    [Header("Appear")]
    private bool isAppear = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        attack();
    }

    private void FixedUpdate()
    {
        if (isAppear)
        {
            Move();
        }
        else
        {
            animator.Play("appear");
            StartCoroutine(appearAnim());    
        }

    }

    //Appear
    IEnumerator appearAnim()
    {
        yield return new WaitForSeconds(2f);
        isAppear = true;        
    }

    //Move
    public override void Move()
    {
        float x_offset = Input.GetAxisRaw("Horizontal");
        float y_offset = Input.GetAxisRaw("Vertical");

        Vector2 moveDirection = new Vector2(x_offset, y_offset);
        rb.velocity = moveDirection * speed;

        if (x_offset != 0)
            transform.localScale = new Vector3(x_offset, 1, 1);

        if (moveDirection != Vector2.zero)
        {
            animator.Play("run");
        }
        else
        {
            rb.velocity = Vector2.zero;
            animator.Play("idle");
        }
    }

    //Attack
    public void attack()
    {
        int dir = UtilsFunc.getKeyboardDir();
        if (dir != -1)
        {
            GameObject bullet = bulletPool.Take();
            bullet.GetComponent<Rigidbody2D>().velocity = Constants.DIRECTIONS_VEC2[dir] * bulletSpeed + rb.velocity * 0.5f;
        }

    }

    //Damaged
    public override void TakeDamage(float damage)
    {
        if (isVulnerable) return;
     
        currHealth -= Mathf.Max((damage - defense), 0);
        if (currHealth < 0)
        {
            OnDead();
            UIManager.instance.updateHpBar();
            return;
        }
        OnVulnerable();
        UIManager.instance.updateHpBar();
    }

    public void OnVulnerable()
    {
        isVulnerable = true;
        animator.Play("vulnerable");
        StartCoroutine(vulnerableTimer());
    }

    private IEnumerator vulnerableTimer()
    {
        yield return new WaitForSeconds(vulnerableDuration);
        isVulnerable = false;
        animator.Play("normal");
    }

    public void OnDead()
    {
        Debug.Log("Game over!");
    }

    //ItemBase
    public void getCoin(int value)
    {
        coins += value;
    }
}
