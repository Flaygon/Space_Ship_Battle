using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Interceptor : Ship {

    protected override void Update()
    {
        base.Update();
    }

    public override void SpeedUp()
    {
        velocity = Mathf.Min(maxVelocity, velocity + velocityPerSecond * 2 * Time.deltaTime);
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
}