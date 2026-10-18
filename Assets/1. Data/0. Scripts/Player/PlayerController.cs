using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 moveInput;
    private float nextFireTime;

    private PlayerInput playerInput;
    private InputAction attackAction;
    private InputAction specialAction;

    [Header("Data Reference")]
    public CharacterData data;
    public bool isPCMode = true;

    [Header("Current Stats")]
    private float fireRate;
    private float moveSpeed;
    private float attackRange;

    [Header("Detection & Setup")]
    public Transform firePoint;
    [SerializeField] private LayerMask enemyLayer;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody>();

        attackAction = playerInput.actions["Attack"];
        specialAction = playerInput.actions["Special"];
    }
    void Start()
    {
        InitializeStats();
    }
    void Update()
    {
        HandleMovement();

        if (specialAction.WasPressedThisFrame())
        {
            UseSpecial();
        }

        if (isPCMode)
        {
            if (attackAction.WasPressedThisFrame())
            {
                ManualAttack();
            }
        }
        else
        {
            AutoTarget_And_Attack();
        }
    }

    void InitializeStats()
    {
        if (data == null) { return; }

        fireRate = data.baseAttackRate;
        moveSpeed = data.baseMoveSpeed;
        attackRange = data.baseAttackRange;
    }
    private void OnDrawGizmos()
    {
        if (firePoint == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(firePoint.position, attackRange);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1f, 0.5f, 0f, 0.5f);

        // Calculate the left and right boundaries of the cone
        Vector3 leftRayDirection = Quaternion.AngleAxis(-data.meleeStats.swingArc / 2, transform.up) * transform.forward;
        Vector3 rightRayDirection = Quaternion.AngleAxis(data.meleeStats.swingArc / 2, transform.up) * transform.forward;

        // Draw the two outer lines
        Gizmos.DrawRay(transform.position, leftRayDirection * attackRange);
        Gizmos.DrawRay(transform.position, rightRayDirection * attackRange);

        // Draw an arc at the end of the range to visualize the "curve"
        int segments = 10;
        Vector3 previousPoint = transform.position + leftRayDirection * attackRange;

        for (int i = 1; i <= segments; i++)
        {
            float currentAngle = -data.meleeStats.swingArc / 2 + (data.meleeStats.swingArc / segments) * i;
            Vector3 nextDir = Quaternion.AngleAxis(currentAngle, transform.up) * transform.forward;
            Vector3 nextPoint = transform.position + nextDir * attackRange;

            Gizmos.DrawLine(previousPoint, nextPoint);
            previousPoint = nextPoint;
        }

        // Optional: Draw a line from origin to center-front for aiming reference
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * attackRange);
    }

    void HandleMovement()
    {
        Vector3 move = new(moveInput.x, -1, moveInput.y);
        rb.linearVelocity = move * moveSpeed;

        if (move != Vector3.zero)
        {
            transform.forward = move;
        }
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    #region Attacking Logic
    public void UseSpecial()
    {
        data.specialAbility.Activate(this.gameObject);
    }

    //Attack Types
    void Attack_MeleeSwing()
    {
        GetComponent<MeleeHandler>().PerformMeleeAttack(firePoint, attackRange, data.baseAttackDamage, data.meleeStats, enemyLayer);
        nextFireTime = Time.time + (1f / fireRate);
    }
    void Attack_Projectile(Transform target, Vector3 fallbackPos)
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

        if (enemies.Length > 0)
        {
            GameObject projObj = Instantiate(data.weaponPrefab, firePoint.position, firePoint.rotation);
            Projectile proj = projObj.GetComponent<Projectile>();

            // If target is null (PC clicking ground), the projectile just flies forward
            proj.target = target;
            if (target == null) projObj.transform.LookAt(fallbackPos);

            nextFireTime = Time.time + (1f / fireRate);

        }
    }

    //PC or Mobile Attack Mode
    void ManualAttack()
    {
        if (Time.time < nextFireTime) return;

        //RAYCAST FROM MOUSE
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            //checkin if we clicked an actual enemy for the projectile to track
            Transform enemyHit = hit.collider.CompareTag("Enemy") ? hit.collider.transform : null;

            Vector3 targetPoint = hit.point;
            targetPoint.y = transform.position.y;

            ExecuteAttack(targetPoint, enemyHit);
        }
    }
    void AutoTarget_And_Attack()
    {
        if (Time.time >= nextFireTime)
        {
            Collider[] enemies = Physics.OverlapSphere(transform.position, attackRange, enemyLayer);

            if (enemies.Length > 0)
            {
                Transform closest = GetClosestEnemy(enemies);
                ExecuteAttack(closest.position, closest);
            }
        }
    }

    Transform GetClosestEnemy(Collider[] enemies)
    {
        Transform bestTarget = null;
        float closestDistance = Mathf.Infinity;
        foreach (var enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < closestDistance)
            {
                closestDistance = dist;
                bestTarget = enemy.transform;
            }
        }
        return bestTarget;
    }
    public void ExecuteAttack(Vector3 targetPos, Transform targetTransform = null)
    {
        Vector3 lookDir = new(targetPos.x, transform.position.y, targetPos.z);
        transform.LookAt(lookDir);

        switch (data.weaponType)
        {
            case WeaponType.Projectile:
                Attack_Projectile(targetTransform, targetPos);
                break;
            case WeaponType.Melee:
                Attack_MeleeSwing();
                break;
            case WeaponType.AoESpray:
                break;
        }
    }
    #endregion
}
