using UnityEngine;

public class TimedLife : MonoBehaviour
{
    public float duration = 8f;
    void Start() => Destroy(gameObject, duration);
}
