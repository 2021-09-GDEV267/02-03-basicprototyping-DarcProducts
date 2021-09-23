using UnityEngine;

public class CreateTimedObject : MonoBehaviour
{
    [SerializeField] GameObject timedObject;
    [SerializeField] float timeToExist;
    [SerializeField] LayerMask createOnHitLayer;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject, createOnHitLayer))
        {
            GameObject newObject = Instantiate(timedObject, transform.position, Quaternion.Euler(0, 0, Random.Range(-180f, 180f)));
            Destroy(newObject, timeToExist);
        }
    }
}