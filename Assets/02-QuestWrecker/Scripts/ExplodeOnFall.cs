using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnFall : MonoBehaviour
{
    [SerializeField] GameObject explodePrefab;
    [SerializeField] LayerMask explodeSelfOnHitLayer;
    [SerializeField] float explodeThreshold;
    [SerializeField] bool useTrigger;
    [Range(0f, 1f)] [SerializeField] float reduceVelValue;
    Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (useTrigger) return;
        Activate(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!useTrigger) return;
        Activate(collision.gameObject);
    }

    void Activate(GameObject obj)
    {
        if (rb.velocity.x > explodeThreshold | rb.velocity.y > explodeThreshold)
        {
            if (Utils.IsInLayerMask(obj, explodeSelfOnHitLayer))
            {
                Instantiate(explodePrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
        Rigidbody2D otherRB = obj.GetComponent<Rigidbody2D>();
        Vector2 otherVel = otherRB.velocity;

        otherVel -= otherVel * reduceVelValue;
        otherRB.velocity = otherVel;
    }
}
