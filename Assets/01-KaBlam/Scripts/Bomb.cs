using UnityEngine;
using UnityEngine.Events;

public class Bomb : MonoBehaviour
{
    public static UnityAction BombMissed;
    public static UnityAction BombExploded;
    [SerializeField] float bottomY = -.5f;
    [SerializeField] Rigidbody2D thisRigidbody;
    SpriteRenderer sRend;

    void Awake()
    {
        sRend = GetComponent<SpriteRenderer>();
        thisRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Bomb.BombMissed += Sleep;
        BombPicker.OnFinishedExplodingBombs += WakeUp;
    }

    private void OnDisable()
    {
        Bomb.BombMissed -= Sleep;
        BombPicker.OnFinishedExplodingBombs -= WakeUp;
    }

    void Start()
    {
        if (sRend != null)
        {
            if (Random.value > .5f)
                sRend.flipX = true;
            else
                sRend.flipX = false;
        }
    }

    void Update()
    {
        if (transform.position.y < bottomY)
        {
            BombMissed?.Invoke();
            BombPicker bpScript = Camera.main.GetComponent<BombPicker>();
            bpScript.BombDestroyed();
            Destroy(gameObject);
        }
    }

    public void IncreaseDropSpeed()
    {
        float gravity = thisRigidbody.gravityScale;
        if (gravity < 2)
            thisRigidbody.gravityScale += .1f;
    }

    private void OnDestroy() => BombExploded?.Invoke();

    void WakeUp() => thisRigidbody.WakeUp();

    void Sleep() => thisRigidbody.Sleep();
}