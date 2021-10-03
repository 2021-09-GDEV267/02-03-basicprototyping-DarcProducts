using UnityEngine;
using UnityEngine.Events;

public class FrogController : MonoBehaviour
{
    [SerializeField] KeyCode moveUp, moveDown, moveLeft, moveRight;
    [SerializeField] Vector2 maxXY;
    [SerializeField] float frogStep;
    [SerializeField] UnityEvent<GameObject> OnFrogMoved;

    void Update()
    {
        if (Input.GetKeyDown(moveUp) && transform.position.y < maxXY.y)
        {
            transform.SetPositionAndRotation(transform.position += Vector3.up * frogStep, Quaternion.Euler(Vector3.zero));
            OnFrogMoved?.Invoke(gameObject);
        }
        if (Input.GetKeyDown(moveDown) && transform.position.y > 0)
        {
            transform.SetPositionAndRotation(transform.position += Vector3.down * frogStep, Quaternion.Euler(0, 0, -180));
            OnFrogMoved?.Invoke(gameObject);
        }
        if (Input.GetKeyDown(moveLeft) && transform.position.x > 0)
        {
            transform.SetPositionAndRotation(transform.position += Vector3.left * frogStep, Quaternion.Euler(0, 0, 90));
            OnFrogMoved?.Invoke(gameObject);
        }
        if (Input.GetKeyDown(moveRight) && transform.position.x < maxXY.x)
        {
            transform.SetPositionAndRotation(transform.position += Vector3.right * frogStep, Quaternion.Euler(0, 0, -90));
            OnFrogMoved?.Invoke(gameObject);
        }
    }

    public void DealWithCurrentTileType()
    {
        var castLoc = transform.position + Vector3.forward;
        RaycastHit2D hit = Physics2D.Raycast(castLoc, transform.position - castLoc, 2);

        if (hit.collider != null)
        {
            var e = hit.collider.GetComponent<ITriggerEvent>();
            if (e != null)
                e.TriggerEvent();
            LevelTile tile = hit.collider.GetComponent<LevelTile>();
            if (tile != null)
                print($"Landed on: {tile.GetTileType()}");
        }
    }

    public void FrogDeath(GameObject notUsed)
    {
        print($"FROG DEATH! {notUsed.name}");
        DestroyImmediate(this.gameObject, true);
    }
}