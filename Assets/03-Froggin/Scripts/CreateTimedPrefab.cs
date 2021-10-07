using UnityEngine;

public class CreateTimedPrefab : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] float prefabLife;
    bool createdPrefab = false;

    public void CreatePrefab(GameObject objLoc)
    {
        if (!createdPrefab)
        {
            GameObject newGO = Instantiate(prefab, objLoc.transform.position, Quaternion.identity);
            Destroy(newGO, prefabLife);
            createdPrefab = true;
            Invoke(nameof(ResetValue), .2f);
        }
    }

    public void CreatePrefab(Vector3 objLoc)
    {
        if (!createdPrefab)
        {
            GameObject newGO = Instantiate(prefab, objLoc, Quaternion.identity);
            Destroy(newGO, prefabLife);
            createdPrefab = true;
            Invoke(nameof(ResetValue), .2f);
        }
    }

    void ResetValue() => createdPrefab = false;
}