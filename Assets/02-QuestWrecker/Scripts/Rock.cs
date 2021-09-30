using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Rock : MonoBehaviour
{
    [SerializeField] GameEvent OnHitOtherObject;
    [SerializeField] float hitThreshold;
    [SerializeField] bool destroyAfterHit;
    [SerializeField] float timeToDestroyAfterHit;
    [SerializeField] float maxDistanceToBroadcastEvent;
    Vector2 startingPos;
    Rigidbody2D RB;

    void Awake() => RB = GetComponent<Rigidbody2D>();

    void Start() => startingPos = transform.position;

    void OnDisable() => CancelInvoke(nameof(Destroy));

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (RB.velocity.magnitude > hitThreshold)
        {
            if (Vector2.Distance(transform.position, startingPos) < maxDistanceToBroadcastEvent)
                OnHitOtherObject?.Invoke();
            if (destroyAfterHit)
                Destroy(gameObject, timeToDestroyAfterHit);
        }
    }
}