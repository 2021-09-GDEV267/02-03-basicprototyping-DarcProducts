using TMPro;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] GameObject explosion;
    [SerializeField] float explosionDisplayTime;
    float cT = 0;
    TMP_Text scoreText;

    void Start()
    {
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        cT = explosionDisplayTime;

        if (scoreGO != null)
        {
            scoreText = scoreGO.GetComponent<TMP_Text>();
            scoreText.text = "0";
        }

        if (explosion != null)
            explosion.SetActive(false);
    }

    void Update()
    {
        if (explosion != null)
        {
            if (explosion.activeSelf && cT > 0)
                cT -= .1f * Time.fixedDeltaTime;
            else if (explosion.activeSelf && cT <= 0)
                explosion.SetActive(false);
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 pos = this.transform.position;
        pos.x = mousePos3D.x;
        this.transform.position = pos;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject collidedWith = col.gameObject;
        if (collidedWith.CompareTag("Bomb"))
        {
            if (explosion != null)
                explosion.SetActive(true);
            cT = explosionDisplayTime;
            Destroy(collidedWith);
            int score = int.Parse(scoreText.text);
            score++;
            scoreText.text = score.ToString();

            if (score > HighScore.score)
                HighScore.score = score;
        }
    }
}