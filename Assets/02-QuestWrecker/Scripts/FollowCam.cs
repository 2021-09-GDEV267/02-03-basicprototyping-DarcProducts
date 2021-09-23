using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public static GameObject POI;
    [Range(0f, .1f)] [SerializeField] float easing = .05f;
    Vector2 minXY = Vector2.zero;
    float camZ;

    void Awake() => camZ = this.transform.position.z;

    void FixedUpdate()
    {
        Vector3 destination;
        if (POI == null)
            destination = Vector2.zero;
        else
        {
            destination = POI.transform.position;

            if (POI.tag == "Projectile")
            {
                if (POI.GetComponent<Rigidbody2D>().IsSleeping())
                {
                    POI = null;
                    return;
                }
            }
        }

        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;

        transform.position = destination;

        Camera.main.orthographicSize = destination.y + 10;
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            POI = null;
    }
}