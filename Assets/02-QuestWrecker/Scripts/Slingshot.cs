using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    public static Slingshot S;
    [Header("Sprite GLow")]
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite slingshotGlow;
    Sprite slingshotOrig;
    [Header("Launch Data")]
    [SerializeField] float magnitudeMult;
    [SerializeField] GameObject launchPoint;
    Vector3 launchPos;
    GameObject projectile;
    bool aimingMode;
    Rigidbody2D projectileRB;
    [Header("Projectile Data")]
    [SerializeField] GameObject prefabProjectile;
    [SerializeField] float velocityMult = 8f;

    void Awake()
    {
        S = this;
        Transform launchPointTrans = launchPoint.transform;
        slingshotOrig = spriteRenderer.sprite;
        launchPoint.SetActive(false);
        launchPos = launchPoint.transform.position;
    }

    void Update()
    {
        if (!aimingMode) return;

        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);
        
        Vector3 mouseDelta = mousePos3D - launchPos;
        float maxMagnitude = this.GetComponent<CircleCollider2D>().radius * magnitudeMult;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        Vector3 projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;

        if (Input.GetMouseButtonUp(0))
        {
            aimingMode = false;
            projectileRB.isKinematic = false;
            projectileRB.velocity = -mouseDelta * velocityMult;
            FollowCam.POI = projectile;
            projectile = null;

            QuestWrecker.ShotFired();
            ProjectileLine.S.poi = projectile;
        }
    }

    public static Vector2 LAUNCH_POS
    {
        get 
        {
            if (S == null)
                return Vector2.zero;
            return S.launchPos;
        }
    }

    void OnMouseEnter()
    {
        print("Slingshot: OnMouseEnter()");
        spriteRenderer.sprite = slingshotGlow;
        launchPoint.SetActive(true);
    }

    void OnMouseExit()
    {
        print("Slingshot: OnMouseExit()");
        spriteRenderer.sprite = slingshotOrig;
        launchPoint.SetActive(false);
    }

    void OnMouseDown()
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile) as GameObject;
        projectile.transform.position = launchPos;
        projectileRB = projectile.GetComponent<Rigidbody2D>();
        projectileRB.isKinematic = true;
    }
}
