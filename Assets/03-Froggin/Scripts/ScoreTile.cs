using UnityEngine;

public class ScoreTile : MonoBehaviour
{
    public void TurnOffCollider(GameObject obj)
    {
        Collider2D col = obj.GetComponent<Collider2D>();
        if (col != null)
        {
            col.enabled = false;
            ScoreCounter.S.IsGameWon();
        }
    }
}
