using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Robber : MonoBehaviour
{
    public static UnityAction OnFinishedBombWave;
    public static UnityAction OnDroppedBomb;
    [SerializeField] float beginingDropDelay = 10f;
    [SerializeField] GameObject bomb;
    [SerializeField] Transform bombDropLoc;
    [SerializeField] float startingSpeed = 1f;
    [SerializeField] float maxSpeed;
    [SerializeField] float leftAndRightEdge;
    [SerializeField] float chanceToChangeDirections = .1f;
    [SerializeField] float secondsBetweenDrops = 1;
    [SerializeField] float minSecondsBetweenDrops;
    [SerializeField] int waveDropNumber;
    float currentSecondsBetweenDrops;
    int currentWave;
    float currentSpeed;
    int currentWaveDropNumber;
    [SerializeField] bool startMoving = true;
    
    void Start()
    {
        currentSpeed = startingSpeed;
        currentWave = 1;
        currentSecondsBetweenDrops = secondsBetweenDrops;
        Rigidbody2D bombRB = bomb.GetComponent<Rigidbody2D>();
        if (bombRB != null)
            bombRB.gravityScale = .1f;
    }

    void Update()
    {
        if (startMoving)
        {
            Vector2 pos = transform.position;
            pos.x += currentSpeed * Time.deltaTime;
            transform.position = pos;

            if (pos.x < -leftAndRightEdge)
                currentSpeed = Mathf.Abs(currentSpeed);
            else if (pos.x > leftAndRightEdge)
                currentSpeed = -Mathf.Abs(currentSpeed);
        }
    }

    void OnEnable() => Invoke(nameof(StartDroppingBombs), beginingDropDelay);

    void OnDisable()
    {
        StopAllCoroutines();
        CancelInvoke(nameof(StartDroppingBombs));
    }

    void FixedUpdate()
    {
        if (startMoving)
            if (Random.value < chanceToChangeDirections)
                currentSpeed *= -1;
    }

    void StartDropping() => StartCoroutine(nameof(DropBombs));

    IEnumerator DropBombs()
    {
        if (bomb != null)
        {
            while (currentWaveDropNumber < waveDropNumber)
            {
                GameObject b = Instantiate(bomb, bombDropLoc.position, Quaternion.identity);
                OnDroppedBomb?.Invoke();
                currentWaveDropNumber++;
                yield return new WaitForSeconds(currentSecondsBetweenDrops);
            } 
        }
        OnFinishedBombWave?.Invoke();
        yield return new WaitForSeconds(2);
        currentWave++;
        currentWaveDropNumber = 0;
        StartCoroutine(nameof(DropBombs));
    }

    public void IncreaseRobberSpeed(float amount)
    {
        float newSpeed = currentSpeed;
        if (newSpeed < 0 && newSpeed - amount < -maxSpeed)
            startingSpeed -= amount;
        if (newSpeed > 0 && newSpeed + amount > maxSpeed)
            startingSpeed += amount;
    }

    public void DecreaseBombDropTime(float amount)
    {
        if (currentSecondsBetweenDrops - amount > minSecondsBetweenDrops)
            currentSecondsBetweenDrops -= amount;
    }

    public void StartDroppingBombs()
    {
        startMoving = true;
        StartCoroutine(nameof(DropBombs));
        currentSpeed = startingSpeed;
    }

    public void StopDroppingBombs()
    {
        startMoving = false;
        StopCoroutine(nameof(DropBombs));
        currentSpeed = 0;
    }

    public void IncreaseWaveDropCount(int amount) => waveDropNumber += amount;
    public void ResetDropCount() => currentWaveDropNumber = 0;
}