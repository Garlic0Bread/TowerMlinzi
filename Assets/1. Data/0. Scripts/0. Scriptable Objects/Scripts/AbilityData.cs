using UnityEngine;

public abstract class AbilityData : ScriptableObject
{
    [SerializeField] private float cooldown;
    [SerializeField] private string abilityName;

    public abstract void Activate(GameObject parent);
}
