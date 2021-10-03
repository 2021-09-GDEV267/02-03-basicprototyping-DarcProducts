using System.Collections.Generic;
using UnityEngine;

public class EventTriggerSwitch : MonoBehaviour
{
    [SerializeField] float rayDistance;
    [SerializeField] bool valueToSet;
    [SerializeField] LayerMask colliderLayer;
    Vector3 castLoc;
    Vector3 hitPoint;


    private void Update()
    {
        castLoc = transform.position + Vector3.forward;
        RaycastHit2D hit = Physics2D.Raycast(castLoc, transform.position - castLoc, rayDistance, colliderLayer);
        if (hit.collider != null)
        {
            var eventTrigger = hit.collider.GetComponent<EventTrigger>();
            if (eventTrigger != null)
                eventTrigger.SetActive(valueToSet);
            hitPoint = hit.point;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(castLoc, hitPoint);
    }
}