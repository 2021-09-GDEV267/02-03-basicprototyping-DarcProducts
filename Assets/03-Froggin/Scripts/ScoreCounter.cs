using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ScoreCounter : MonoBehaviour
{
    public static ScoreCounter S;
    [SerializeField] int maxFrogs = 12;
    int currentFrog = 0;
    [SerializeField] UnityEvent OnGameOver;
    [SerializeField] UnityEvent OnGameWon;
    [SerializeField] TMP_Text frogCountText;
    int frogsHome = 0;

    void Awake() => S = this;

    void Start() => frogCountText.text = $"Lives Left: {maxFrogs - currentFrog}";

    public bool UpdateFrogDeathCount()
    {
        currentFrog++;
        frogCountText.text = $"Lives Left: {maxFrogs - currentFrog}";
        if (currentFrog >= maxFrogs)
        {
            OnGameOver?.Invoke();
            return false;
        }
        return true;
    }

    public bool IsGameWon()
    {
        frogsHome++;
        if (frogsHome >= 5)
        {
            OnGameWon?.Invoke();
            return true;
        }
        return false;
    }
}