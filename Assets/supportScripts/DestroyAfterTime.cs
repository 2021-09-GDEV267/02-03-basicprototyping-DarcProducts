using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] float timeToDestroy = 1;

    void Start() => Destroy(gameObject, timeToDestroy);
}