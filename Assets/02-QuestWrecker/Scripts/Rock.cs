using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Rock : MonoBehaviour
{
    [SerializeField] GameEvent OnHitOtherObject;
    [SerializeField] float hitThreshold;
    [SerializeField] bool destroyAfterHit;
    [SerializeField] float timeToDestroyAfterHit;
    Rigidbody2D RB;

    void Awake() => RB = GetComponent<Rigidbody2D>();

    void OnDisable() => CancelInvoke(nameof(Destroy));

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (RB.velocity.magnitude > hitThreshold)
        {
            OnHitOtherObject?.Invoke();
            if (destroyAfterHit)
                Destroy(gameObject, timeToDestroyAfterHit);
        }
    }
}