using UnityEngine;
using UnityEngine.Events;

public class Turtle : MonoBehaviour
{
    public UnityEvent OnTurtleAvailable;
    public UnityEvent OnTurtleUnavailable;
    Animator animator;

    void Awake() => animator = GetComponent<Animator>();

    private void Start()
    {
        int ranNum = Random.Range(0, 3);
        switch (ranNum)
        {
            case 0: animator.SetTrigger("Fast");
                break;
            case 1:
                animator.SetTrigger("Slow");
                break;
            case 2:
                animator.SetTrigger("Medium");
                break;
            default: Debug.LogError($"{gameObject.name} unable to set animation!");
                break;
        }
    }

    public void OnAvailable() => OnTurtleAvailable?.Invoke();

    public void OnUnavailable() => OnTurtleUnavailable?.Invoke();
}