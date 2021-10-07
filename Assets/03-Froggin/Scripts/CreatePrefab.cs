using UnityEngine;

public class CreatePrefab : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    public void Create(GameObject obj) => Instantiate(prefab, obj.transform.position, Quaternion.identity);
}