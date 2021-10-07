using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomizeSpriteColor : MonoBehaviour
{
    void OnEnable() => GetComponent<SpriteRenderer>().color = new Color32((byte)Random.Range(40, 256), (byte)Random.Range(40, 256), (byte)Random.Range(40, 256), 255);
}