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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject, frogLayer))
            eventTrigger.TriggerEvent();
    }
}