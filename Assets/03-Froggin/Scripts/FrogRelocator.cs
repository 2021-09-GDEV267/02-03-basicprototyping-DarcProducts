using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogRelocator : MonoBehaviour
{
    GameObject frog = null;
    [SerializeField] LayerMask frogLayer;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (Utils.IsInLayerMask(collision.gameObject, frogLayer))
        {
            frog = collision.gameObject;
            frog.transform.position = new Vector2(transform.position.x, frog.transform.position.y);
        }
    }
}
