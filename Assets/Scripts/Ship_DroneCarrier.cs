using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_DroneCarrier : Ship {

    public DroneManager_Offensive offensiveDrones;
    public DroneManager_Defensive defensiveDrones;

    protected override void Update()
    {
        base.Update();

        if (Input.GetMouseButton(defensiveDrones.mouseButton))
        {
            defensiveDrones.Defend(Camera.main.transform.forward);
        }
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            offensiveDrones.SetTarget(null);
        }
        else if (Input.GetMouseButtonDown(offensiveDrones.mouseButton))
        {
            Transform target = GetComponent<AimingController_Lockon>().target;
            if (target)
            {
                offensiveDrones.SetTarget(target);
            }
        }
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
    }

    public override void FireLeftBroadside()
    {
    }

    public override void FireRightBroadside()
    {
    }
}