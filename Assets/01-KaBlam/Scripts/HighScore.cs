using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public static int score = 100;
    TMP_Text gt;

    void Awake()
    {
        gt = this.GetComponent<TMP_Text>();
        if (PlayerPrefs.HasKey("HighScore"))
            score = PlayerPrefs.GetInt("HighScore");
        PlayerPrefs.SetInt("HighScore", score);
    }

    private void Update()
    {
        gt.text = $"High Score: {score}";
        if (score > PlayerPrefs.GetInt("HighScore"))
            PlayerPrefs.SetInt("HighScore", score);
    }
}