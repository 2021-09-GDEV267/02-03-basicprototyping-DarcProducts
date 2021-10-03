using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    [SerializeField] bool useX, useY;

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos);

        Vector3 pos = this.transform.position;
        if (useX)
            pos.x = mousePos3D.x;
        if (useY)
            pos.y = mousePos3D.y;
        this.transform.position = pos;
    }
}