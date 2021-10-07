using UnityEngine;

public class EventTriggerSwitch : MonoBehaviour
{
    [SerializeField] bool setValue;
    [SerializeField] LayerMask triggerLayers;

    void Update()
    {
        Vector3 rayPos = transform.position + Vector3.back;
        RaycastHit2D hit = Physics2D.Raycast(rayPos, transform.position - rayPos, 2f, triggerLayers);

        if (hit.collider != null)
        {
            EventTrigger trigger = hit.collider.GetComponent<EventTrigger>();
            if (trigger != null)
                trigger.SetActive(setValue);
        }
    }

    public void SetSwitchValue(bool newSetValue) => setValue = newSetValue;
}