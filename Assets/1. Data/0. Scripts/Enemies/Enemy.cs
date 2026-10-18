using System.Collections;
using UnityEngine;

public enum EnemyState { Moving, Attacking, Stunned }

public class EnemyAI : MonoBehaviour
{
    private float _currentMoveSpeed;
    public float CurrentMoveSpeed { get => _currentMoveSpeed; set => _currentMoveSpeed = value; }
    public EnemyTemplate template;
    protected float currentHealth;
    protected float attackRange;
    protected float attackRate;

    [Header("References")]
    protected Rigidbody rb;
    protected Transform target;
    protected EnemyState currentState = EnemyState.Moving;

    void InitializeFromTemplate()
    {
        _currentMoveSpeed = template.moveSpeed;
        attackRange = template.attackRange;
        attackRate = template.attackRate;

        Health h = GetComponent<Health>();
        h.maxHealth = template.maxHealth;
        h.currentHealth = template.maxHealth;
    }

    void Start()
    {
        InitializeFromTemplate();

        rb = GetComponent<Rigidbody>();
        target = GameObject.FindGameObjectWithTag("Tower").transform;
    }
    void Update()
    {
        if (currentState == EnemyState.Stunned) return;

        float distToTarget = Vector3.Distance(transform.position, target.position);

        if (distToTarget <= attackRange)
        {
            currentState = EnemyState.Attacking;
            OnReachTarget();
        }
        else
        {
            MoveTowardTarget();
        }
    }

    protected virtual void MoveTowardTarget()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        dir.y = 0;
        rb.linearVelocity = dir * _currentMoveSpeed;
        transform.forward = dir;
    }
    protected virtual void OnReachTarget()
    {
        // This is overridden by specific enemy types
    }

    public void ApplySlow(float multiplier, float duration)
    {
        StartCoroutine(SlowRoutine(multiplier, duration));
    }
    private IEnumerator SlowRoutine(float m, float d)
    {
        float originalSpeed = _currentMoveSpeed;
        _currentMoveSpeed *= m;
        yield return new WaitForSeconds(d);
        _currentMoveSpeed = originalSpeed;
    }
}