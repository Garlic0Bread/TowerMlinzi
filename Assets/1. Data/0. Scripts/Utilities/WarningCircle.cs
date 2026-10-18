using UnityEngine;
using UnityEngine.UI;

public class WarningCircle : MonoBehaviour
{
    public SpriteRenderer fillSprite;
    private float timer = 0f;
    private float duration;

    public void Setup(float radius, float time)
    {
        duration = time;

        transform.localScale = new Vector3(radius * 2, radius * 2, 1);
        if (fillSprite != null) fillSprite.transform.localScale = Vector3.zero;
    }

    void Update()
    {
        timer += Time.deltaTime;
        float progress = Mathf.Clamp01(timer / duration);

        if (fillSprite != null)
        {
            fillSprite.transform.localScale = new Vector3(progress, progress, 1);
        }

        if (progress >= 1f)
        {
            Destroy(gameObject, 0.5f);
        }
    }
}
