using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSpriteFlip : MonoBehaviour
{
    void OnEnable()
    {
        SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        if (Random.value > .5f)
            spriteRend.flipX = true;
        else
            spriteRend.flipX = false;
    }
}