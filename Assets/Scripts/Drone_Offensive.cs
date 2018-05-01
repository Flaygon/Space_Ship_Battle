using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Offensive : Drone
{
    public Projectile projectileAsset;

    public Transform target;

    public Transform cannonMouth;

    public FiringController firingController;

    protected override void Update()
    {
        if(target)
        {
            HandleOrbital(target);

            Vector3 distance = target.position - transform.position;
            if (distance.magnitude <= orbitalRadius)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(distance.normalized, Vector3.up), rotationPerSecond);
                if(Vector3.Dot(distance.normalized, transform.forward) >= 0.95)
                {
                    firingController.Fire(cannonMouth, projectileAsset);
                }
            }
        }
        else
        {
            HandleOrbital(manager.transform);
        }
    }
}