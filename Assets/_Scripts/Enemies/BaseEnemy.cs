using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthManager))]
[RequireComponent(typeof(Animator))]
public class BaseEnemy : PoolableObject
{
    [SerializeField] BaseEnemyScriptable _settings;
    [SerializeField] string FireBonusKey;
    [SerializeField] string GlueBonusKey;
    private int moneyValue;
    private float speed;
    private bool attacking;
    private AudioSource audio;
    private HealthManager healthManager;
    private GameManager gm;
    private Coroutine currentActivityCoroutine;
    private Animator animator;

    private float previousSpeed;
    private bool isSlowDown = false;

    private Coroutine burningCoroutine;
    // Start is called before the first frame update
    void Awake()
    {
        audio = gameObject.GetComponent<AudioSource>();
        healthManager = GetComponent<HealthManager>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        speed = _settings.baseSpeed;
    }

    private void OnEnable()
    {
        currentActivityCoroutine = StartCoroutine(MoveForward());
        attacking = false;
    }

    public void InitEnemy(float bonusHealth, float bonusSpeed, float bonusValue)
    {
        InitHealth(_settings.baseHealth * bonusHealth);
        moneyValue = Mathf.RoundToInt(_settings.baseValue * bonusValue);
        speed = _settings.baseSpeed * bonusSpeed;
    }

    private void InitHealth(float health)
    {
        healthManager.SetUp(health);
        healthManager.NoHealth += Die;
    }

    private void Die()
    {
        StopCoroutine(currentActivityCoroutine);
        if(burningCoroutine != null)
        {
            StopCoroutine(burningCoroutine);
        }
        attacking = false;
        gm.Die(moneyValue);
        animator.SetBool("Dies", true);
        gameObject.SetActive(false);
    }

    public void TakeDamage(float damage)
    {
        healthManager.TakeDamage(damage);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "tower")
        {
            attacking = true;
            StopCoroutine(currentActivityCoroutine);
            currentActivityCoroutine = StartCoroutine(AttackCoroutine(collision));
        }
    }

    private IEnumerator MoveForward()
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x - (Time.deltaTime * speed), transform.position.y, transform.position.z);
            yield return null;
        }
    }

    private IEnumerator AttackCoroutine(Collision2D collision)
    {
        while (true)
        {
            // perform attack
            audio.Play();
            collision.gameObject.GetComponent<HealthManager>().TakeDamage(_settings.enemyDamage);
            yield return new WaitForSeconds(_settings.attackCooldown);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "fire")
        {
            TakeDamage(Time.deltaTime * collision.GetComponent<Fire>().baseDamage * PlayerPrefs.GetFloat(FireBonusKey));
        }
        if (collision.gameObject.tag == "glue" & !isSlowDown)
        {
            isSlowDown = true;
            previousSpeed = speed;
            speed = speed - PlayerPrefs.GetFloat(GlueBonusKey);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gameObject.active == false) { return; }
        if (collision.gameObject.tag == "glue")
        {
            isSlowDown = false;
            speed = previousSpeed;
        }
        if (collision.gameObject.tag == "fire")
        {
            if (burningCoroutine != null)
            {
                StopCoroutine(burningCoroutine);
            }
            burningCoroutine = StartCoroutine(Burn(collision));
        }
    }

    private IEnumerator Burn(Collider2D collision)
    {
        float totalTime = 0;
        while (totalTime < collision.GetComponent<Fire>().burningTime)
        {
            TakeDamage(Time.deltaTime * collision.GetComponent<Fire>().baseDamage * PlayerPrefs.GetFloat(FireBonusKey));
            totalTime += Time.deltaTime;
            yield return null;
        }
        burningCoroutine = null;
    }
}
