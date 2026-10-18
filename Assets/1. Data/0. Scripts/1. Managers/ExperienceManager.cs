using UnityEngine;

public class ExperienceManager : MonoBehaviour
{
    public static ExperienceManager Instance;

    [Header("Current Run Data")]
    public CharacterData activeCharacter;
    public float expGainedThisRun;

    void Awake() => Instance = this;

    public void AddRenown(float amount)
    {
        expGainedThisRun += amount;
        // Optional: Show a little "+10 XP" text popup above the player
    }

    public void FinalizeRun()
    {
        // Call this when the 10 mins are up or the Tower falls
        activeCharacter.AddExp(expGainedThisRun);
        Debug.Log("Run Saved. Total Exp Gained: " + expGainedThisRun);
    }
}
