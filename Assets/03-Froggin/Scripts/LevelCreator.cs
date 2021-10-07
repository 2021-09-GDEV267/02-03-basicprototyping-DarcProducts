using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] float startDelay;
    [SerializeField] GameObject gravelPrefab, gravelEdge, roadPrefab, waterPrefab, grassEdge, grassPrefab;
    Vector2 levelWidthHeight = new Vector2(36, 17);
    Vector2 spawnXLocations = new Vector2(-2, 36);
    [Range(1, 16)] [SerializeField] byte distanceSafePath;
    [Tooltip("Random between these values")] [SerializeField] Vector2 roadPathSpawnTimes;
    [Tooltip("Random between these values")] [SerializeField] Vector2 waterPathSpawnTimes;
    [SerializeField] Slider difficultySlider;

    [Header(">----- Water -----<")]
    [SerializeField] GameObject[] waterObjectsMovingLeft;

    [SerializeField] GameObject[] waterObjectsMovingRight;

    [Header(">----- Road -----<")]
    [SerializeField] GameObject[] roadObjectsMovingLeft;

    [SerializeField] GameObject[] roadObjectsMovingRight;

    [SerializeField] UnityEvent OnGameStart;

    private void Update()
    {
        if (difficultySlider != null)
            if (difficultySlider.gameObject.activeSelf)
                distanceSafePath = (byte)difficultySlider.value;
    }

    public void InitializeGame() => StartCoroutine(nameof(StartGame));

    IEnumerator StartGame()
    {
        BuildLevel();
        Time.timeScale = 10;
        yield return new WaitForSecondsRealtime(startDelay);
        Time.timeScale = 1;
        OnGameStart?.Invoke();
    }

    [ContextMenu("Build Level")]
    void BuildLevel()
    {
        int currentWaterPath = distanceSafePath + 1;
        var spawnLoc = new Vector2(-2, 36);
        bool sendRight = true;

        for (int i = 0; i < levelWidthHeight.x; i++)
        {
            for (int j = 0; j < 17; j++)
            {
                if (j.Equals(0))
                    SpawnTile(i, j, gravelPrefab);
                else if (j < distanceSafePath && j > 0)
                {
                    SpawnTile(i, j, roadPrefab);
                    if (i.Equals(0))
                    {
                        if (sendRight)
                            StartCoroutine(SpawnPathObject((int)spawnLoc.x, j, roadObjectsMovingRight, roadPathSpawnTimes));
                        else
                            StartCoroutine(SpawnPathObject((int)spawnLoc.y, j, roadObjectsMovingLeft, roadPathSpawnTimes));
                        sendRight = !sendRight;
                    }
                }
                else if (j < 17 && !j.Equals(distanceSafePath))
                {
                    SpawnTile(i, j, waterPrefab);
                    if (i == levelWidthHeight.x - 1)
                    {
                        if (sendRight)
                            StartCoroutine(SpawnPathObject((int)spawnLoc.x, currentWaterPath, waterObjectsMovingRight, waterPathSpawnTimes));
                        else
                            StartCoroutine(SpawnPathObject((int)spawnLoc.y, currentWaterPath, waterObjectsMovingLeft, waterPathSpawnTimes));
                        sendRight = !sendRight;
                        currentWaterPath++;
                    }
                }
                else if (j.Equals(distanceSafePath))
                    SpawnTile(i, j, gravelEdge);
            }
        }
    }

    void SpawnTile(int x, int y, GameObject prefab) => Instantiate(prefab, new Vector2(x, y), Quaternion.identity, transform);

    IEnumerator SpawnPathObject(int xLoc, int yLoc, GameObject[] objs, Vector2 spawnTimes)
    {
        Instantiate(objs[Random.Range(0, objs.Length)], new Vector2(xLoc, yLoc), Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(spawnTimes.x, spawnTimes.y));
        StartCoroutine(SpawnPathObject(xLoc, yLoc, objs, spawnTimes));
    }
}