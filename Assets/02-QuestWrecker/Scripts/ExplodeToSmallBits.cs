using UnityEngine;

public class ExplodeToSmallBits : MonoBehaviour
{
    [SerializeField] GameObject explodedPrefab;
    [SerializeField] LayerMask explodeOnHitLayer;
    [SerializeField] float explodeThreshold;
    [SerializeField] bool useTrigger;
    [Range(0f, 1f)] [SerializeField] float reduceVelValue;
    Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (useTrigger) return;
        Activate(collision.gameObject);
        if (rb.velocity.x > explodeThreshold | rb.velocity.y > explodeThreshold)
        {
            Rigidbody2D otherRB = collision.gameObject.GetComponent<Rigidbody2D>();
            if (otherRB != null)
            {
                Vector2 otherVel = otherRB.velocity;
                otherVel -= otherVel * reduceVelValue;
                otherRB.velocity = otherVel;
            }
            Instantiate(explodedPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!useTrigger) return;
        Activate(collision.gameObject);
    }

    void Activate(GameObject obj)
    {
        Rigidbody2D otherRB = obj.GetComponent<Rigidbody2D>();
        if (otherRB != null)
        {
            Vector2 otherVel = otherRB.velocity;

            if (otherVel.x > explodeThreshold | otherVel.y > explodeThreshold)
            {
                if (Utils.IsInLayerMask(obj, explodeOnHitLayer))
                {
                    Instantiate(explodedPrefab, transform.position, transform.rotation);
                    otherVel -= otherVel * reduceVelValue;
                    otherRB.velocity = otherVel;
                    Destroy(gameObject);
                }
            }
        }
    }
}