using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ExplodeToSmallBits : MonoBehaviour
{
    [SerializeField] GameObject explodedPrefab;
    [SerializeField] LayerMask explodeOnHitLayer;
    [SerializeField] float explodeThreshold;
    [SerializeField] float fallVelThreshold;
    [Range(0f, 1f)] [SerializeField] float reduceVelValue;
    [SerializeField] bool useDynamicResponse = false;
    public GameEvent OnExploded;
    Rigidbody2D rb;

    void Awake() => rb = GetComponent<Rigidbody2D>();

    void OnCollisionEnter2D(Collision2D collision)
    {
        SlowOtherObject(collision.gameObject);
        TryExplode(collision.gameObject);
    }

    void SlowOtherObject(GameObject obj)
    {
        Rigidbody2D otherRB = obj.GetComponent<Rigidbody2D>();
        {
            if (otherRB != null)
            {
                Vector2 otherVel = otherRB.velocity;
                otherVel -= otherVel * reduceVelValue;
                otherRB.velocity = otherVel;
            }
        }
    }

    void TryExplode(GameObject obj)
    {
        if (Utils.IsInLayerMask(obj, explodeOnHitLayer))
        {
            Rigidbody2D objRB = obj.GetComponent<Rigidbody2D>();
            if (objRB != null)
            {
                if (objRB.velocity.magnitude > explodeThreshold || rb.velocity.magnitude > fallVelThreshold)
                {
                    Instantiate(explodedPrefab, transform.position, transform.rotation);
                    if (useDynamicResponse)
                        OnExploded?.Invoke(gameObject);
                    else
                        OnExploded?.Invoke();
                    Destroy(gameObject);
                }
            }
        }
    }
}