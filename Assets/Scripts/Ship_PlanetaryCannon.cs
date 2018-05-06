using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_PlanetaryCannon : Ship
{
    public bool detached = true;

    public float forcePerShot;

    public int bodyDamage;

    public Rigidbody body;

    [HideInInspector]
    public Vector3 attachedNormal;

    protected override void Update()
    {
        if(!detached)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                detached = true;

                body.velocity = Camera.main.transform.forward * maxVelocity * 0.5f;
                transform.forward = -transform.forward;
            }
        }
    }

    public override void SpeedUp()
    {
        //velocity = Mathf.Min(maxVelocity, velocity + velocityPerSecond * Time.deltaTime);
    }

    public override void SlowDown()
    {
        //velocity = Mathf.Max(0, velocity + -velocityPerSecond * Time.deltaTime);
    }

    public override void RotateLeft()
    {
        //transform.Rotate(0.0f, -rotationPerSecond * Time.deltaTime, 0.0f, Space.Self);
    }

    public override void RotateRight()
    {
        //transform.Rotate(0.0f, rotationPerSecond * Time.deltaTime, 0.0f, Space.Self);
    }

    public override void TiltLeft()
    {
        //transform.Rotate(0.0f, 0.0f, rotationPerSecond * Time.deltaTime, Space.Self);
    }

    public override void TiltRight()
    {
        //transform.Rotate(0.0f, 0.0f, -rotationPerSecond * Time.deltaTime, Space.Self);
    }

    public override void TiltForward()
    {
        //transform.Rotate(rotationPerSecond * Time.deltaTime, 0.0f, 0.0f, Space.Self);
    }

    public override void TiltBackward()
    {
        //transform.Rotate(-rotationPerSecond * Time.deltaTime, 0.0f, 0.0f, Space.Self);
    }

    public override void Fire()
    {
        if (detached && firingController.CanFire())
        {
            Vector3 newVelocity = body.velocity;
            newVelocity += -leftCannonMouths[0].forward * forcePerShot;
            if (newVelocity.magnitude > maxVelocity)
            {
                newVelocity = newVelocity.normalized * maxVelocity;
            }
            body.velocity = newVelocity;
        }

        firingController.Fire(leftCannonMouths, projectileAsset);
    }

    public override void FireLeftBroadside()
    {
        firingController.Fire(leftCannonMouths, projectileAsset);
    }

    public override void FireRightBroadside()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (detached && collision.gameObject.transform.parent)
        {
            Ship ship = collision.gameObject.transform.parent.GetComponent<Ship>();
            Drone drone = collision.gameObject.transform.parent.GetComponent<Drone>();
            if (ship)
            {
                ship.Damage(bodyDamage);
            }
            else if (drone)
            {
                drone.Damage(bodyDamage);

                health -= drone.health;
                if (health <= 0)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                detached = false;

                body.velocity = Vector3.zero;
                body.angularVelocity = Vector3.zero;
                transform.position = collision.contacts[0].point;
                transform.forward = collision.contacts[0].normal;

                attachedNormal = transform.forward;
            }
        }
    }
}