using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingRope : MonoBehaviour
{
    [SerializeField] GrapplingGun grappling;
    [SerializeField] AnimationCurve curve;
    [SerializeField] int quality;
    [SerializeField] float damper;
    [SerializeField] float strength;
    [SerializeField] float velocity;
    [SerializeField] float waveCount;
    [SerializeField] float waveHeight;

    private Spring spring;
    private Vector3 currentGrapplePosition;
    private LineRenderer lr;

    void Awake()
    {
        lr = GetComponent<LineRenderer>();
        spring = new Spring();
        spring.SetTarget(0);
    }

    void LateUpdate()
    {
        DrawRope();
    }

    void DrawRope()
    {
        //If not grappling, don't draw rope
        if (!grappling.IsGrappling()) {
            currentGrapplePosition = grappling.gunTip.position;
            spring.Reset();
            if (lr.positionCount > 0) lr.positionCount = 0;
            return;
        }

        if(lr.positionCount == 0)
        {
            spring.SetVelocity(velocity);
            lr.positionCount = quality + 1;
        }

        spring.SetDamper(damper);
        spring.SetStrength(strength);
        spring.Update(Time.deltaTime);

        Vector3 grapplePoint = grappling.GetGrapplePoint();
        Vector3 gunTipPosition = grappling.gunTip.position;
        Vector3 up = Quaternion.LookRotation(grapplePoint - gunTipPosition).normalized * Vector3.up;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition,
            grapplePoint, Time.deltaTime * 12f);

        for(int i = 0; i < quality + 1; i++)
        {
            float delta = i / quality;
            Vector3 offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI)
                * spring.Value * curve.Evaluate(delta);

            lr.SetPosition(i, Vector3.Lerp(gunTipPosition, currentGrapplePosition, delta) + offset);
        }
        
    }

}
