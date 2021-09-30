using UnityEngine;

public class ExplodeOnDisable : MonoBehaviour
{
    [SerializeField] float explosionRadius;
    [SerializeField] float explosionForce;

    private void OnDisable()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

        foreach (Collider2D c in colliders)
        {
            Rigidbody2D tRB = c.attachedRigidbody;
            if (tRB != null)
                tRB.AddForce(explosionForce * (c.transform.position - transform.position), ForceMode2D.Impulse);
        }
    }
}