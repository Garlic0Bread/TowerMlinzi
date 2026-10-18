using UnityEngine;
using UnityEngine.VFX;

public enum CharacterClass { TribeWarrior, SpiritualWarrior }
public enum WeaponType { Projectile, Melee, AoESpray, Homing }

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Game/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Identity")]
    public string tribe;
    public string characterName;
    public Sprite characterPortrait;
    public CharacterClass characterClass;

    [Header("Base Stats")]
    public float baseHealth = 100f;
    public float baseMoveSpeed = 5f;
    public float baseAttackRate = 1f;
    public float baseAttackRange = 5f;
    public float baseAttackDamage = 10f;

    [Header("Equipment & Abilities")]
    public WeaponType weaponType;
    public MeleeStats meleeStats;
    public GameObject weaponPrefab;   // The Spear, Arrow, or Orb
    public AbilityData passiveAbility;
    public AbilityData specialAbility;

    [Header("Progression")]
    public int currentLevel = 1;
    public float currentExp = 0;
    public CharacterLevelBenefit[] levelUpUnlocks;

    public float GetRequiredExp()
    {
        // Formula: 100, 250, 450, 700...
        return 100 * Mathf.Pow(currentLevel, 1.5f);
    }
    public void AddExp(float amount)
    {
        currentExp += amount;
        while (currentExp >= GetRequiredExp())
        {
            currentExp -= GetRequiredExp();
            currentLevel++;
            Debug.Log($"{characterName} reached Level {currentLevel}!");
        }
    }
}

[System.Serializable]
public struct CharacterLevelBenefit
{
    public int level;
    public string description;
    public float statMultiplier; // e.g., 1.2f for 20% boost
}

[System.Serializable]
public struct MeleeStats
{
    public float swingArc;      // 180 degrees for mace, 30 for spear thrust
    public float knockbackForce;
    public float hitStunDuration;
    public GameObject swingEffect; // Particle system for the "slash"
}