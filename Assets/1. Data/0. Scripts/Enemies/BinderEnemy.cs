using UnityEngine;

public class BinderEnemy : ShooterEnemy// Inherits shooting logic,
{
    //projectile (The Net) prefab has a script: 
    // OnTriggerEnter: if(other.CompareTag("Player")) other.GetComponent<PlayerController>().ApplySlow(0.5f, 2s);
}
