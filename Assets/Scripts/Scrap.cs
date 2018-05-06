using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap : MonoBehaviour {

    public int value;

    private Ship tagger;

    public float velocityPerSecond;
    private float velocity;

    public float minInitialVelocity;
    public float maxInitialVelocity;

    private Vector3 initialDirection;

    public Vector3 minInitialRotation;
    public Vector3 maxInitialRotation;
    private Vector3 initialRotation;

    private void Start()
    {
        velocity = Random.Range(minInitialVelocity, maxInitialVelocity);
        initialDirection = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;

        initialRotation = new Vector3(Random.Range(minInitialRotation.x, maxInitialRotation.x), Random.Range(minInitialRotation.y, maxInitialRotation.y), Random.Range(minInitialRotation.z, maxInitialRotation.z));
    }

    private void Update()
    {
        Vector3 currentDirection;
        if(tagger)
        {
            velocity += velocityPerSecond * Time.deltaTime;
            currentDirection = (tagger.transform.position - transform.position).normalized;

            if(Vector3.Dot((tagger.transform.position - transform.position).normalized, (tagger.transform.position - (transform.position + currentDirection * velocity * Time.deltaTime)).normalized) <= 0.25f)
            {
                tagger.GetComponent<Ship_Crasher>().AdjustScrap(value);
                Destroy(gameObject);
                return;
            }
        }
        else
        {
            velocity = Mathf.Max(0.0f, velocity - velocityPerSecond * Time.deltaTime);
            currentDirection = initialDirection;
        }

        transform.position += currentDirection * velocity * Time.deltaTime;
        transform.Rotate(initialRotation);
    }

    public void Tag(Ship tagger)
    {
        this.tagger = tagger;
    }
}