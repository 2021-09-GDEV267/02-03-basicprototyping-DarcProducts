using UnityEngine;

public class DestroyPastLocation : MonoBehaviour
{
    [SerializeField] LayerMask destroyedLayers;

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject otherGO = collision.gameObject;
        GameObject otherParent = otherGO.transform.parent.gameObject;
        if (Utils.IsInLayerMask(otherGO, destroyedLayers))
        {
            Destroy(otherGO);
            if (otherParent != null)
                Destroy(otherParent);
        }
    }
}