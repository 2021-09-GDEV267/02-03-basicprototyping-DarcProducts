using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TimedSpriteSwitch : MonoBehaviour
{
    [SerializeField] Sprite tempSprite;
    Sprite originalSprite;
    SpriteRenderer spriteRenderer;

    void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();

    void Start() => originalSprite = spriteRenderer.sprite;

    public void Trigger(float duration) => StartCoroutine(ShowTempSprite(duration));

    IEnumerator ShowTempSprite(float duration)
    {
        spriteRenderer.sprite = tempSprite;
        yield return new WaitForSeconds(duration);
        spriteRenderer.sprite = originalSprite;
    }
}