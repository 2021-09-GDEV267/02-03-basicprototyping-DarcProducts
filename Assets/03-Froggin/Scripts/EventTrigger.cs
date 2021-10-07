using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    [SerializeField] bool isActive = true;
    [SerializeField] GameEvent triggeredGameEvent;

    public void TriggerEvent()
    {
        if (isActive)
        {
            if (triggeredGameEvent != null)
                triggeredGameEvent?.Invoke(gameObject);
        }
    }

    public void TriggerEvent(GameObject obj)
    {
        if (isActive)
        {
            if (triggeredGameEvent != null)
                triggeredGameEvent?.Invoke(obj);
        }
    }

    public void SetActive(bool newValue) => isActive = newValue;

    public bool GetActive() => isActive;

    public void PrintConsoleMessage(string message) => print(message);
}