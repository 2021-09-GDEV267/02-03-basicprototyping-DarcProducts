using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class BombPicker : MonoBehaviour
{
    public static UnityAction OnGameFinished;
    public static UnityAction OnFinishedExplodingBombs;
    [SerializeField] GameObject basketPrefab;
    [SerializeField] int numBaskets = 3;
    [SerializeField] float basketBottomY;
    [SerializeField] float basketSpacingY;
    [SerializeField] float delayBetweenWaves;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] float delayBetweenExplosions;
    [SerializeField] TMP_Text menuText;
    List<GameObject> basketList;

    void Start()
    {
        menuText.gameObject.SetActive(false);
        Time.timeScale = 1;
        basketList = new List<GameObject>(0);
        for (int i = 0; i < numBaskets; i++)
        {
            GameObject basketGO = Instantiate(basketPrefab, transform.position, Quaternion.identity);
            basketGO.transform.SetParent(transform);
            Vector2 pos = Vector2.zero;
            pos.y = basketBottomY + basketSpacingY * i;
            basketGO.transform.position = pos;
            basketList.Add(basketGO);
        }
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
            menuText.gameObject.SetActive(true);
        else
            menuText.gameObject.SetActive(false);
    }

    public void BombDestroyed() => StartCoroutine(ExplodeAllBombs());

    IEnumerator<WaitForSeconds> ExplodeAllBombs()
    {
        if (basketList.Count > 0)
        {
            GameObject basketGO = basketList[0];
            basketList.Remove(basketGO);
            Destroy(basketGO);
        }
        if (basketList.Count.Equals(0))
        {
            Time.timeScale = 0;
            OnGameFinished?.Invoke();
        }

        GameObject[] activeBombs = GameObject.FindGameObjectsWithTag("Bomb");
        for (int i = 0; i < activeBombs.Length; i++)
        {
            GameObject cBomb = activeBombs[i];
            Vector2 bPos = activeBombs[i].transform.position;
            Destroy(cBomb);
            GameObject e = Instantiate(explosionPrefab, bPos, Quaternion.identity);
            Destroy(e, .8f);
            yield return new WaitForSeconds(delayBetweenExplosions);
        }
        yield return new WaitForSeconds(delayBetweenWaves);
        OnFinishedExplodingBombs?.Invoke();
    }
}