using UnityEngine;
using System.Collections;

public class DamageVisuals : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    private Color originalColor;

    void Start()
    {
        originalColor = spriteRenderer.color;
    }

    public void Flash()
    {
        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        spriteRenderer.color = Color.white;

        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = originalColor;
    }
}
