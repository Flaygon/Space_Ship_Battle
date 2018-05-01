using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    private Vector3 startPosition;

    public float velocity = 100;

    public float killDistance = 1000;

    public ParticleSystem explosionAsset;

    public int health;
    public int damage;

    public void Initialize()
    {
        startPosition = transform.position;

        GetComponent<Rigidbody>().velocity = transform.forward * velocity;
    }

    private void Update()
    {
        if((startPosition - transform.position).magnitude > killDistance)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.transform.parent)
        {
            Ship ship = collision.gameObject.transform.parent.GetComponent<Ship>();
            Drone drone = collision.gameObject.transform.parent.GetComponent<Drone>();
            if (ship)
            {
                ship.Damage(damage);
                Destroy(gameObject);
            }
            else if(drone)
            {
                drone.Damage(damage);

                health -= drone.health;
                if(health <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
        else
        {
            // Object
            Destroy(gameObject);
        }

        ParticleSystem deathExplosion = Instantiate(explosionAsset);
        deathExplosion.transform.position = transform.position;
        deathExplosion.transform.forward = collision.contacts[0].normal;
    }
}