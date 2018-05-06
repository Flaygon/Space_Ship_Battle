using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship_Crasher : Ship {

    private int currentScrap = 0;
    public int scrapMax;

    public float minSize;
    public float maxSize;

    public float minVelocityPerSecond;
    public float maxVelocityPerSecond;

    public float minSuperVelocityPerSecond;
    public float maxSuperVelocityPerSecond;

    public float minMaxVelocity;
    public float maxMaxVelocity;

    public float minMaxSuperVelocity;
    public float maxMaxSuperVelocity;

    public float minRotationPerSecond;
    public float maxRotationPerSecond;

    public float minSuperRotationPerSecond;
    public float maxSuperRotationPerSecond;

    public int minDamageOnImpact;
    public int maxDamageOnImpact;

    private bool superSpeed;

    public float scrapCollectionRadius;

    public ParticleSystem collisionParticleAsset;

    protected override void Update()
    {
        superSpeed = Input.GetKey(KeyCode.Space);

        if(!superSpeed)
        {
            base.Update();
        }
        else
        {
            velocity = Mathf.Min(CalculateMaxSuperVelocity(), velocity + CalculateSuperVelocityPerSecond() * Time.deltaTime);
            transform.position += transform.forward * velocity * Time.deltaTime;
        }

        if(Input.mouseScrollDelta.magnitude > 0)
        {
            AdjustScrap(Mathf.RoundToInt(Input.mouseScrollDelta.y));
        }

        Collider[] objects = Physics.OverlapSphere(transform.position, scrapCollectionRadius);
        if(objects.Length > 0)
        {
            foreach(Collider iObject in objects)
            {
                Scrap scrap = iObject.transform.GetComponent<Scrap>();
                if(scrap)
                {
                    scrap.Tag(this);
                }
            }
        }
    }

    public override void SpeedUp()
    {
        if(!superSpeed)
            velocity = Mathf.Min(CalculateMaxVelocity(), velocity + CalculateVelocityPerSecond() * Time.deltaTime);
    }

    public override void SlowDown()
    {
        if(!superSpeed)
            velocity = Mathf.Max(0, velocity + -CalculateVelocityPerSecond() * Time.deltaTime);
    }

    public override void RotateLeft()
    {
        if(superSpeed)
        {
            transform.Rotate(0.0f, -CalculateSuperRotationPerSecond() * Time.deltaTime, 0.0f, Space.Self);
        }
        else
        {
            transform.Rotate(0.0f, -CalculateRotationPerSecond() * Time.deltaTime, 0.0f, Space.Self);
        }
    }

    public override void RotateRight()
    {
        if (superSpeed)
        {
            transform.Rotate(0.0f, CalculateSuperRotationPerSecond() * Time.deltaTime, 0.0f, Space.Self);
        }
        else
        {
            transform.Rotate(0.0f, CalculateRotationPerSecond() * Time.deltaTime, 0.0f, Space.Self);
        }
    }

    public override void TiltLeft()
    {
        if (superSpeed)
        {
            transform.Rotate(0.0f, 0.0f, CalculateSuperRotationPerSecond() * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Rotate(0.0f, 0.0f, CalculateRotationPerSecond() * Time.deltaTime, Space.Self);
        }
    }

    public override void TiltRight()
    {
        if (superSpeed)
        {
            transform.Rotate(0.0f, 0.0f, -CalculateSuperRotationPerSecond() * Time.deltaTime, Space.Self);
        }
        else
        {
            transform.Rotate(0.0f, 0.0f, -CalculateRotationPerSecond() * Time.deltaTime, Space.Self);
        }
    }

    public override void TiltForward()
    {
        if (superSpeed)
        {
            transform.Rotate(CalculateSuperRotationPerSecond() * Time.deltaTime, 0.0f, 0.0f, Space.Self);
        }
        else
        {
            transform.Rotate(CalculateRotationPerSecond() * Time.deltaTime, 0.0f, 0.0f, Space.Self);
        }
    }

    public override void TiltBackward()
    {
        if (superSpeed)
        {
            transform.Rotate(-CalculateSuperRotationPerSecond() * Time.deltaTime, 0.0f, 0.0f, Space.Self);
        }
        else
        {
            transform.Rotate(-CalculateRotationPerSecond() * Time.deltaTime, 0.0f, 0.0f, Space.Self);
        }
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

    private float CalculateVelocityPerSecond()
    {
        return Mathf.Lerp(maxVelocityPerSecond, minVelocityPerSecond, CalculateScrapFraction());
    }

    private float CalculateSuperVelocityPerSecond()
    {
        return Mathf.Lerp(maxSuperVelocityPerSecond, minSuperVelocityPerSecond, CalculateScrapFraction());
    }

    private float CalculateMaxVelocity()
    {
        return Mathf.Lerp(maxMaxVelocity, minMaxVelocity, CalculateScrapFraction());
    }

    private float CalculateMaxSuperVelocity()
    {
        return Mathf.Lerp(maxMaxSuperVelocity, minMaxSuperVelocity, CalculateScrapFraction());
    }

    private float CalculateRotationPerSecond()
    {
        return Mathf.Lerp(maxRotationPerSecond, minRotationPerSecond, CalculateScrapFraction());
    }

    private float CalculateSuperRotationPerSecond()
    {
        return Mathf.Lerp(maxSuperRotationPerSecond, minSuperRotationPerSecond, CalculateScrapFraction());
    }

    private int CalculateDamageOnImpact()
    {
        return Mathf.FloorToInt(Mathf.Lerp(minDamageOnImpact, maxDamageOnImpact, CalculateScrapFraction()));
    }

    private float CalculateSize()
    {
        return Mathf.Lerp(minSize, maxSize, CalculateScrapFraction());
    }

    private float CalculateScrapFraction()
    {
        return currentScrap / (float)scrapMax;
    }

    public void AdjustScrap(int scrapValue)
    {
        currentScrap = Mathf.Max(0, Mathf.Min(scrapMax, currentScrap + scrapValue));

        float newSize = CalculateSize();
        transform.localScale = new Vector3(newSize, newSize, newSize);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.transform.parent)
        {
            Ship ship = collision.gameObject.transform.parent.GetComponent<Ship>();
            Drone drone = collision.gameObject.transform.parent.GetComponent<Drone>();
            if (ship)
            {
                ship.Damage(CalculateDamageOnImpact());
            }
            else if (drone)
            {
                drone.Damage(CalculateDamageOnImpact());
            }

            Instantiate(collisionParticleAsset, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
        }
    }
}