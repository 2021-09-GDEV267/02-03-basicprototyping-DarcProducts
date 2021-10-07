using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class RandomizeSprite : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;

    private void OnEnable()
    {
        int ranSprite = Random.Range(0, sprites.Length);
        GetComponent<SpriteRenderer>().sprite = sprites[ranSprite];
    }
}
