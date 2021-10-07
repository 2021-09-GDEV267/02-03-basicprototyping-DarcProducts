using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;

public class FrogController : MonoBehaviour
{
    [SerializeField] KeyCode moveUp, moveDown, moveLeft, moveRight;
    [SerializeField] Vector2 maxXY;
    [SerializeField] float frogStep;
    [SerializeField] GameObject deadFrog;
    [SerializeField] UnityEvent<GameObject> OnFrogMoved;
    [SerializeField] LayerMask tileLayers;
    [SerializeField] GameObject indicator;
    [SerializeField] float indicatorDuration;
    [SerializeField] GameObject menuCanvas;
    Collider2D col;
    SpriteRenderer spriteRenderer;
    bool canMove = false;
    Vector2 origPos;

    void Start()
    {
        origPos = transform.position;
        col = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        menuCanvas.SetActive(false);
        canMove = false;
    }

    void Update()
    {
        if (canMove)
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
        if (Input.GetKeyDown(KeyCode.Tab))
            menuCanvas.SetActive(true);
        if (Input.GetKeyUp(KeyCode.Tab))
            menuCanvas.SetActive(false);
    }

    void LateUpdate()
    {
        if (transform.position.x < 0)
        {
            Vector3 newPos = new Vector3(0, transform.position.y, transform.position.z);
            transform.position = newPos;
        }
        if (transform.position.x > maxXY.x)
        {
            Vector3 newPos = new Vector3(maxXY.x, transform.position.y, transform.position.z);
            transform.position = newPos;
        }

        if (canMove)
            DealWithCurrentTileType();
    }

    public void DealWithCurrentTileType()
    {
        var castLoc = transform.position + Vector3.back;
        RaycastHit2D hit = Physics2D.Raycast(castLoc, transform.position - castLoc, 2, tileLayers);

        if (hit.collider != null)
        {
            var e = hit.collider.GetComponent<EventTrigger>();
            if (e != null)
                e.TriggerEvent();
        }
    }

    public void FrogDeath(GameObject deathObj)
    {
        print($"FROG DEATH! from: {deathObj.name} at {transform.position}");
        canMove = false;
        col.enabled = false;
        spriteRenderer.enabled = false;
        if (!ScoreCounter.S.UpdateFrogDeathCount())
            Destroy(gameObject);
        Invoke(nameof(ResetToOriginalPosition), 1);
    }

    public void ResetToOriginalPosition()
    {
        StartCoroutine(nameof(SetIndicatorOn));
        transform.position = origPos;
        col.enabled = true;
        spriteRenderer.enabled = true;
        canMove = true;
    }

    IEnumerator SetIndicatorOn()
    {
        indicator.SetActive(true);
        yield return new WaitForSecondsRealtime(indicatorDuration);
        indicator.SetActive(false);
    }

    public void SetCanMove(bool newValue) => canMove = newValue;

    public void PlaceFlattenedFrog() => Instantiate(deadFrog, transform.position, Quaternion.identity);
}