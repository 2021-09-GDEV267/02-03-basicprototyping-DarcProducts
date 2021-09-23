using UnityEngine;

public class Goal : MonoBehaviour
{
    static public bool goalMet = false;

    void OnDisable() => goalMet = true;
}