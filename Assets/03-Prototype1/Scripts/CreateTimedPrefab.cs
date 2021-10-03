using UnityEngine;

public class CreateTimedPrefab : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    public void CreatePrefab(float duration)
    {
        GameObject newGO = Instantiate(prefab, transform.position, Quaternion.identity);
        Destroy(newGO, duration);
    }
}