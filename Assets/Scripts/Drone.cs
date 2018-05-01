using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public DroneManager manager;

    public int health;

    public float velocityPerSecond;
    [HideInInspector]
    public float velocity;
    public float maxVelocity;

    public float rotationPerSecond;

    public float orbitalRadius;
    public float erraticnessPerSecond;
    private Vector3 velocityDirection;

    private void Start()
    {
        velocity = maxVelocity;
        velocityDirection = transform.forward;
    }

    protected virtual void Update()
    {
        transform.position += transform.forward * velocity * Time.deltaTime;
    }

    protected void HandleOrbital(Transform target)
    {
        velocityDirection += new Vector3(Random.Range(-erraticnessPerSecond * Time.deltaTime, erraticnessPerSecond * Time.deltaTime),
            Random.Range(-erraticnessPerSecond * Time.deltaTime, erraticnessPerSecond * Time.deltaTime),
            Random.Range(-erraticnessPerSecond * Time.deltaTime, erraticnessPerSecond * Time.deltaTime));

        if ((transform.position - target.position).magnitude > orbitalRadius)
        {
            velocityDirection += (target.position - transform.position).normalized * Time.deltaTime;
        }
        else
        {
            velocityDirection += (transform.position - target.position).normalized * Time.deltaTime;
        }

        velocityDirection = velocityDirection.normalized;
        transform.position += velocityDirection * velocity * Time.deltaTime;
    }

    public void Damage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            manager.Remove(this);
            Destroy(gameObject);
        }
    }
}