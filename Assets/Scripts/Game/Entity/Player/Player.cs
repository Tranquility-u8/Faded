using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Dungeon;
using AllIn1SpriteShader;


public class Player : Entity, IAttackable, IDamageable
{

    public static Player instance;
    Rigidbody2D rb;
    private bool isWalk = false;

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

    [Header("Buff")]
    public BuffPool buffPool;

    [Header("Light")]
    public Transform flashlight;
    public float targetAngle;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        coins = PlayerPrefs.GetInt("coins");
        UIManager.instance.updateCoinBar(coins);
        StartCoroutine(OnNaturalRecovery());
    }

    void Update()
    {
        sprite.sortingOrder = Mathf.RoundToInt(transform.position.y * -100);
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

        //Move the player
        Vector2 moveDirection = new Vector2(x_offset, y_offset);
        rb.velocity = moveDirection * speed;

        //Rotate the flashlight
        float currAngle = flashlight.eulerAngles.z;
        if (moveDirection != Vector2.zero)
        {
            if(!isWalk)
                AudioManager.instance.OnStartWalk();
            isWalk = true;

            if (Mathf.Abs(x_offset) > Mathf.Abs(y_offset))
            {
                if (x_offset > 0)
                {
                    animator.Play("right");
                    targetAngle = 90f;
                }
                else
                {
                    animator.Play("left");
                    targetAngle = -90f;
                }

            }
            else
            {
                if (y_offset > 0)
                {
                    animator.Play("back");
                    targetAngle = 180f;
                }
                else
                {
                    animator.Play("forward");
                    targetAngle = 0f;

                }
            }
        }
        else
        {
            isWalk = false;
            AudioManager.instance.OnStopWalk();
        }

        if(currAngle != targetAngle)
        {
            float newAngle = Mathf.LerpAngle(currAngle, targetAngle, speed * Time.deltaTime);
            flashlight.eulerAngles = new Vector3(transform.eulerAngles.x, flashlight.eulerAngles.y, newAngle);
        }


    }

    //Attack
    public void attack()
    {
        int dir = UtilsFunc.getKeyboardDir();
        if (dir != -1)
        {
            GameObject bullet = bulletPool.Take();

            Vector2 inertia;
            if (dir == 0 || dir == 2)
            {
                inertia = new Vector3(rb.velocity.x, 0);
            }
            else
            {
                inertia = new Vector3(0, rb.velocity.y);
            }
            inertia *= 0.5f;
             
            bullet.GetComponent<Rigidbody2D>().velocity = Constants.DIRECTIONS_VEC2[dir] * bulletSpeed + inertia;
        }

    }

    public void calculateDamage()
    {
        float tmp = baseDamage;
        if (buffPool.checkBuff("Myopia"))
        {
            tmp *= 1.5f;
        }
        actualDamage = tmp;
    }

    public void calculateDefense()
    {
        float tmp = baseDefense;
        actualDefense = tmp;
    }

    //TakeDamage
    public override void TakeDamage(float actualDamage)
    {
        if (isVulnerable) return;
     
        currHealth -= Mathf.Max((actualDamage - actualDefense), 0);
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
        UIManager.instance.onDeathBar();
    }

    //Recovery
    IEnumerator OnNaturalRecovery()
    {
        currHealth = Mathf.Min(currHealth + 1, maxHealth);
        currMana = Mathf.Min(currMana + 2, maxMana);
        UIManager.instance.updateHpBar();
        UIManager.instance.updateMpBar();
        yield return new WaitForSeconds(5.0f);
        StartCoroutine(OnNaturalRecovery());
    }

    IEnumerator OnCustomHpRecovery(float timeout, float gain)
    {
        currHealth = Mathf.Min(currHealth + gain, maxHealth);
        UIManager.instance.updateHpBar();
        yield return new WaitForSeconds(timeout);
        StartCoroutine(OnCustomHpRecovery(timeout, gain));
    }

    IEnumerator OnCustomMpRecovery(float timeout, float gain)
    {
        currMana = Mathf.Min(currMana + gain, maxMana);
        UIManager.instance.updateMpBar();
        yield return new WaitForSeconds(timeout);
        StartCoroutine(OnCustomMpRecovery(timeout, gain));

    }

    public void OnPromptHpRecovery(float gain)
    {
        currHealth = Mathf.Min(currHealth + gain, maxHealth);
        UIManager.instance.updateHpBar();
    }

    private void OnDestroy()
    {
        PlayerPrefs.SetInt("coins", coins);
    }

    //ItemBase
    public void getCoin(int value)
    {
        coins += value;
        UIManager.instance.updateCoinBar(coins);
    }

    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }

}
