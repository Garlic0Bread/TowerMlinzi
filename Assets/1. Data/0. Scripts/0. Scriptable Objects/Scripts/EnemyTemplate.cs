using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Game/Enemy Template")]
public class EnemyTemplate : ScriptableObject
{
    [Header("Identity")]
    public string enemyName;
    public Sprite enemyIcon;

    [Header("Base Stats")]
    public float maxHealth = 50f;
    public float moveSpeed = 3f;
    public float attackRange = 2f;
    public float attackRate = 1.5f;
    public float attackDamage = 10f;

    [Header("Rewards")]
    public int renownXP = 10;
    public int cowrieDropMin = 1;
    public int cowrieDropMax = 3;

    [Header("Behavior Flags")]
    public bool ignoresPlayer = false; // For Sprinters
    public bool isExplosive = false;   // For Sappers/Grunts
    public bool hasShield = false;      // For Gatekeepers
}
