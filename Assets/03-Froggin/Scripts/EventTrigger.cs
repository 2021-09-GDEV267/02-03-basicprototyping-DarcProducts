using UnityEngine;
using UnityEngine.Events;

public class EventTrigger : MonoBehaviour, ITriggerEvent
{
    [SerializeField] UnityEvent<GameObject> OnTriggeredEvent;
    [SerializeField] bool isActive = true;
    [SerializeField] GameEvent triggeredGameEvent;

    public void TriggerEvent()
    {
        if (isActive)
        {
            OnTriggeredEvent?.Invoke(gameObject);
            
            if (triggeredGameEvent != null)
                triggeredGameEvent?.Invoke(gameObject);
        }
    }

    public void SetActive(bool newValue) => isActive = newValue;

    public void PrintConsoleMessage(string message) => print(message);
}