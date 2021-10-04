using UnityEngine;

public class CreatePrefab : MonoBehaviour
{
    [SerializeField] GameObject prefab;

    public void Create(float duration) => Instantiate(prefab, transform.position, Quaternion.identity);
}