using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class Snake : MonoBehaviour
{
    [Range(0f, 100f)] [SerializeField] float chanceToSurvive;
    [SerializeField] float speed;
    SpriteRenderer sR;
    Vector3 origLocPos;

    void Awake() => sR = GetComponent<SpriteRenderer>();

    void OnEnable()
    {
        origLocPos = transform.localPosition;
        if (Random.value * 100 > chanceToSurvive)
            Destroy(gameObject);
    }

    void FixedUpdate()
    {
        if (transform.localPosition.x < origLocPos.x - 1 | transform.localPosition.x > origLocPos.x + 1)
            speed *= -1;
        transform.Translate(new Vector3(speed * Time.fixedDeltaTime, 0, transform.localPosition.z));
        switch (speed)
        {
            case 1: sR.flipX = false;
                break;
            case -1: sR.flipX = true;
                break;
            default: print("Eww!");
                break;
        }
    }
}
