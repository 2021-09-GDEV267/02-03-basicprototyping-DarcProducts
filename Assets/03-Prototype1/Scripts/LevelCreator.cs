using System.Collections;
using UnityEngine;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] GameObject gravelPrefab, gravelEdge, roadPrefab, waterPrefab, grassEdge, grassPrefab;
    Vector2 levelDimensions = new Vector2(36, 20);

    [Header(">----- Water -----<")]
    [SerializeField] GameObject[] waterPathObjects;

    [SerializeField] Transform[] waterPathSpawnLocations;
    [SerializeField] float[] waterPathSpawnTimes;

    [Header(">----- Road -----<")]
    [SerializeField] GameObject[] roadPathObjects;

    [SerializeField] Transform[] roadPathSpawnLocations;
    [SerializeField] float[] roadPathSpawnTimes;

    void Start()
    {
        BuildLevel();
        StartWaterObjects();
        StartRoadObjects();
    }

    [ContextMenu("Build Level")]
    public void BuildLevel()
    {
        for (int i = 0; i < levelDimensions.x; i++)
        {
            for (int j = 0; j < levelDimensions.y; j++)
            {
                if (j.Equals(0))
                    SpawnTile(i, j, gravelPrefab);
                else if (j < 8 && j > 0)
                    SpawnTile(i, j, roadPrefab);
                else if (j < 17 && !j.Equals(8))
                    SpawnTile(i, j, waterPrefab);
                else if (j.Equals(8))
                    SpawnTile(i, j, gravelEdge);
                else if (j.Equals(17))
                    SpawnTile(i, j, grassEdge);
                else
                    SpawnTile(i, j, grassPrefab);
            }
        }
    }

    [ContextMenu("Start Water Objects")]
    public void StartWaterObjects()
    {
        for (int i = 0; i < waterPathObjects.Length; i++)
            StartCoroutine(SpawnWaterPathObject(i, i, waterPathSpawnTimes[i]));
    }

    public void SpawnTile(int x, int y, GameObject prefab) => Instantiate(prefab, new Vector2(x, y), Quaternion.identity, transform);

    public IEnumerator SpawnWaterPathObject(int indx, int path, float duration)
    {
        if (indx < waterPathObjects.Length && path < waterPathSpawnLocations.Length)
        {
            if (waterPathObjects[indx] != null)
            {
                Instantiate(waterPathObjects[indx], waterPathSpawnLocations[path].position, Quaternion.identity, transform);
                yield return new WaitForSeconds(duration);
                StartCoroutine(SpawnWaterPathObject(indx, path, duration));
            }
        }
    }

    [ContextMenu("Start Road Objects")]
    public void StartRoadObjects()
    {
        for (int i = 0; i < roadPathObjects.Length; i++)
            StartCoroutine(SpawnRoadPathObject(i, i, roadPathSpawnTimes[i]));
    }

    public IEnumerator SpawnRoadPathObject(int indx, int path, float duration)
    {
        if (indx < roadPathObjects.Length && path < roadPathSpawnLocations.Length)
        {
            if (roadPathObjects[indx] != null)
            {
                Instantiate(roadPathObjects[indx], roadPathSpawnLocations[path].position, Quaternion.identity, transform);
                yield return new WaitForSeconds(duration);
                StartCoroutine(SpawnRoadPathObject(indx, path, duration));
            }
        }
    }
}