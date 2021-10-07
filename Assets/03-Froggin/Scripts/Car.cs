using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField] LayerMask frogLayer;
    [SerializeField] EventTrigger eventTrigger;

    void Start()
    {
        SpriteRenderer sR = GetComponent<SpriteRenderer>();
        sR.color = new Color32((byte)Random.Range(20, 255), (byte)Random.Range(20, 255), (byte)Random.Range(20, 255), 255);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject, frogLayer))
        {
            collision.enabled = false;
            eventTrigger.TriggerEvent(new GameObject());
        }
    }
}