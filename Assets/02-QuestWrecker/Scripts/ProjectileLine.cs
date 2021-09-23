using System.Collections.Generic;
using UnityEngine;

public class ProjectileLine : MonoBehaviour
{
    public static ProjectileLine S;
    [SerializeField] float minDist = .1f;
    LineRenderer line;
    GameObject _poi;
    List<Vector2> points;

    void Awake()
    {
        S = this;
        line = GetComponent<LineRenderer>();
        line.enabled = false;
        points = new List<Vector2>();
    }

    void FixedUpdate()
    {
        if (poi == null)
        {
            if (FollowCam.POI != null)
            {
                if (FollowCam.POI.CompareTag("Projectile"))
                    poi = FollowCam.POI;
                else
                    return;
            }
            else return;
        }
        AddPoint();
        if (FollowCam.POI == null)
            poi = null;
    }

    public GameObject poi
    {
        get { return _poi; }
        set
        {
            _poi = value;
            if (_poi != null)
            {
                line.enabled = false;
                points = new List<Vector2>();
                AddPoint();
            }
        }
    }

    public void Clear()
    {
        _poi = null;
        line.enabled = false;
        points = new List<Vector2>();
    }

    public void AddPoint()
    {
        Vector2 pt = _poi.transform.position;

        if (points.Count > 0 && (pt - lastPoint).magnitude < minDist)
            return;
        if (points.Count == 0)
        {
            Vector2 launchPosDiff = pt - Slingshot.LAUNCH_POS;
            points.Add(pt + launchPosDiff);
            points.Add(pt);
            line.positionCount = 2;
            line.SetPosition(0, points[0]);
            line.SetPosition(1, points[1]);
            line.enabled = true;
        }
        else
        {
            points.Add(pt);
            line.positionCount = points.Count;
            line.SetPosition(points.Count - 1, lastPoint);
            line.enabled = true;
        }
    }

    public Vector2 lastPoint
    {
        get
        {
            if (points == null)
                return Vector2.zero;
            return points[points.Count - 1];
        }
    }
}