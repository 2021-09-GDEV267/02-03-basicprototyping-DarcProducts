using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOtherOverTime : MonoBehaviour
{
    [SerializeField] Vector2 travelVector;


    void FixedUpdate() => transform.position += (Vector3)travelVector * Time.fixedDeltaTime;
}
