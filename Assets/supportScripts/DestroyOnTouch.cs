using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    [SerializeField] LayerMask destroyOnHitLayer;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject, destroyOnHitLayer))
            Destroy(gameObject);
    }
}