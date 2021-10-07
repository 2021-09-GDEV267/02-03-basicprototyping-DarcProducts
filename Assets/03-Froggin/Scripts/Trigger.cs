using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] LayerMask detectLayers;
    [SerializeField] bool valueToSet;
    [SerializeField] EventTrigger eventTrigger;

    void OnCollisionStay2D(Collision2D collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject, detectLayers))
        {
            eventTrigger.SetActive(valueToSet);
            eventTrigger.TriggerEvent();
        }
    }
}