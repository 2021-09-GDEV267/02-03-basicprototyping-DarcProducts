using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public static GameObject POI;
    [Range(0f, .1f)] [SerializeField] float easing = .05f;
    [SerializeField] float maxFollowRange;
    Vector2 minXY = Vector2.zero;
    float camZ;

    void Awake() => camZ = this.transform.position.z;

    void FixedUpdate()
    {
        Vector3 destination;
        if (POI == null)
            return;
        else
        {
            if (QuestWrecker.S.GetCurrentView().Equals("Show Both"))
                return;

            if (POI.CompareTag("Projectile"))
            {
                float pXPos = POI.transform.position.x;
                if (POI.GetComponent<Rigidbody2D>().IsSleeping() || pXPos > maxFollowRange)
                {
                    POI = Slingshot.S.gameObject;
                    return;
                }
            }
            destination = POI.transform.position;
        }

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;

        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;
    }
}