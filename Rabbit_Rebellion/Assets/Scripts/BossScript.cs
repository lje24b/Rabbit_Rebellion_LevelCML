using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class BossController : MonoBehaviour
{
    [Header("Animator parameters")]
    public Animator animator;

    [Tooltip("If true the script will use AttackIndex + AttackTrigger. If false, it will use the individual triggers in attackTriggerNames.")]
    public bool useAttackIndex = true;

    [Tooltip("Name of the trigger used to start an attack (used with AttackIndex).")]
    public string attackTriggerName = "Attack";

    [Tooltip("Name of the int parameter used to select which attack (0..2).")]
    public string attackIndexParam = "AttackIndex";

    [Tooltip("If not using index, list the trigger names for Attack 0,1,2 respectively.")]
    public string[] attackTriggerNames = new string[] { "Attack1", "Attack2", "Attack3" };

    [Header("Attack timing")]
    public float timeBetweenAttacks = 2f;      
    public bool autoAttack = false;            
    public bool randomizeAttacks = false;      

    [Header("Health")]
    public int maxHealth = 100;
    int currentHealth;

    [Header("Death")]
    public string dieTriggerName = "Die";
    public float disableAfterDeathDelay = 2f;  

    bool canAttack = true;
    bool isDead = false;
    Coroutine autoAttackRoutine = null;

    void Awake()
    {
        if (animator == null) animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Start()
    {
        if (autoAttack)
        {
            autoAttackRoutine = StartCoroutine(AutoAttackLoop());
        }
    }

    public void DoAttack(int attackIndex)
    {
        if (isDead || !canAttack) return;

        attackIndex = Mathf.Clamp(attackIndex, 0, 2);

        if (useAttackIndex)
        {
            animator.SetInteger(attackIndexParam, attackIndex);
            animator.SetTrigger(attackTriggerName);
        }
        else
        {
            if (attackTriggerNames != null && attackTriggerNames.Length > attackIndex && !string.IsNullOrEmpty(attackTriggerNames[attackIndex]))
                animator.SetTrigger(attackTriggerNames[attackIndex]);
            else
                Debug.LogWarning($"BossController: attackTriggerNames not set for index {attackIndex}");
        }

        StartCoroutine(AttackCooldown());
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(timeBetweenAttacks);
        canAttack = true;
    }

    IEnumerator AutoAttackLoop()
    {
        yield return new WaitForSeconds(1.0f);

        while (!isDead)
        {
            if (canAttack)
            {
                int idx = randomizeAttacks ? Random.Range(0, 3) : 0;
                if (!randomizeAttacks)
                {
                    idx = (int)(Time.time / timeBetweenAttacks) % 3;
                }

                DoAttack(idx);
            }
            yield return null;
        }
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        animator.SetTrigger(dieTriggerName);

        if (autoAttackRoutine != null) StopCoroutine(autoAttackRoutine);

        StartCoroutine(DisableAfterDelay(disableAfterDeathDelay));
    }

    IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    public void OnAttackEvent_SpawnProjectile(int attackIndex)
    {
        Debug.Log($"Spawn projectile for attack {attackIndex}");
    }
}
