using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteFlip : MonoBehaviour
{
    void OnEnable()
    {
        SpriteRenderer spriteRend = GetComponent<SpriteRenderer>();
        if (spriteRend != null)
        {
            if (Random.value > .5f)
                spriteRend.flipX = true;
            else
                spriteRend.flipX = false;
        }
    }
}
