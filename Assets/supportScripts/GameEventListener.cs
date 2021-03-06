using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent Event;
    public UnityEvent Response;
    public UnityEvent<GameObject> gameobjectDynamicResponse;

    void OnEnable() => Event.RegisterListener(this);
    void OnDisable() => Event.UnregisterListener(this);
    public void OnEventRaised() => Response?.Invoke();
    public void OnEventRaised(GameObject obj) => gameobjectDynamicResponse?.Invoke(obj);
}
