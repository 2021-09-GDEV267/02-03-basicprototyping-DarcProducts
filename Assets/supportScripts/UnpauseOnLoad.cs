using UnityEngine;

public class UnpauseOnLoad : MonoBehaviour
{
    void OnEnable() => Time.timeScale = 1;
}