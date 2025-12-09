using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class BossController : MonoBehaviour
{
    [Header("Animator Parameters")]
    public Animator animator;

    [Tooltip("Use attack index to select attacks via a single trigger.")]
    public bool useAttackIndex = true;

    public string attackTriggerName = "Attack";
    public string attackIndexParam = "AttackIndex";

    [Tooltip("If not using index, list individual trigger names.")]
    public string[] attackTriggerNames = new string[] { "Attack1", "Attack2", "Attack3" };

    [Header("Attack Timing")]
    public float timeBetweenAttacks = 2f;
    public bool autoAttack = true;
    public bool randomizeAttacks = false;

    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("Death")]
    public string dieTriggerName = "Die";
    public float disableAfterDeathDelay = 2f;

    private bool canAttack = true;
    private bool isDead = false;
    private Coroutine autoAttackRoutine = null;

    private int attackCounter = 0; // Tracks which attack to use next

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
            if (attackTriggerNames != null && attackTriggerNames.Length > attackIndex &&
                !string.IsNullOrEmpty(attackTriggerNames[attackIndex]))
            {
                animator.SetTrigger(attackTriggerNames[attackIndex]);
            }
            else
            {
                Debug.LogWarning($"BossController: attackTriggerNames not set for index {attackIndex}");
            }
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
        yield return new WaitForSeconds(1f);

        while (!isDead)
        {
            if (canAttack)
            {
                int idx;
                if (randomizeAttacks)
                {
                    idx = Random.Range(0, 3);
                }
                else
                {
                    idx = attackCounter % 3;
                    attackCounter++;
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

        if (autoAttackRoutine != null)
            StopCoroutine(autoAttackRoutine);

        StartCoroutine(DisableAfterDelay(disableAfterDeathDelay));
    }

    IEnumerator DisableAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
    }

    // ===== AnimationEvent Functions =====
    // These are called from AnimationEvents in your animation clips
    public void OnAttackEvent_SpawnProjectile(int attackIndex)
    {
        Debug.Log($"[Boss] Spawn projectile for attack {attackIndex}");
        // TODO: spawn projectile prefab depending on attackIndex
    }

    public void OnAttackEvent_DealDamage(int attackIndex)
    {
        Debug.Log($"[Boss] Deal damage for attack {attackIndex}");
        // TODO: apply damage to player
    }
}
