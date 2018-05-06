using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Bombadier : Ship {

    protected override void Update()
    {
        base.Update();
    }

    public override void SpeedUp()
    {
        velocity = Mathf.Min(maxVelocity, velocity + velocityPerSecond * Time.deltaTime);
    }

    public override void SlowDown()
    {
        velocity = Mathf.Max(0, velocity + -velocityPerSecond * Time.deltaTime);
    }

    public override void RotateLeft()
    {
        transform.Rotate(0.0f, -rotationPerSecond * Time.deltaTime, 0.0f, Space.Self);
    }

    public override void RotateRight()
    {
        transform.Rotate(0.0f, rotationPerSecond * Time.deltaTime, 0.0f, Space.Self);
    }

    public override void TiltLeft()
    {
        transform.Rotate(0.0f, 0.0f, rotationPerSecond * Time.deltaTime, Space.Self);
    }

    public override void TiltRight()
    {
        transform.Rotate(0.0f, 0.0f, -rotationPerSecond * Time.deltaTime, Space.Self);
    }

    public override void TiltForward()
    {
        transform.Rotate(rotationPerSecond * Time.deltaTime, 0.0f, 0.0f, Space.Self);
    }

    public override void TiltBackward()
    {
        transform.Rotate(-rotationPerSecond * Time.deltaTime, 0.0f, 0.0f, Space.Self);
    }

    public override void Fire()
    {
        if (Vector3.Dot(transform.right, Camera.main.transform.forward) >= 0)
        {
            FireRightBroadside();
        }
        else
        {
            FireLeftBroadside();
        }
    }

    public override void FireLeftBroadside()
    {
        firingController.Fire(leftCannonMouths, projectileAsset);
    }

    public override void FireRightBroadside()
    {
        firingController.Fire(rightCannonMouths, projectileAsset);
    }
}