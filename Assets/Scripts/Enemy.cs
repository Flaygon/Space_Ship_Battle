using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public GameManager manager;

    public Ship ship;

    public float fireSpeedPerSecond = 2;
    private float currentFireSpeed;

    public float firingRange = 200.0f;

    private void Update()
    {
        if (ship == null)
        {
            manager.enemies.Remove(this);
            Destroy(gameObject);
            return;
        }

        currentFireSpeed += Time.deltaTime;

        Vector3 playerDistance = manager.player.ship.transform.position - ship.transform.position;
        float upDot = Vector3.Dot(playerDistance.normalized, ship.transform.up);
        float rightDot = Vector3.Dot(playerDistance.normalized, ship.transform.right);
        float forwardDot = Vector3.Dot(playerDistance.normalized, ship.transform.forward);
        if (rightDot >= 0)
        {
            if(rightDot >= 0.9 && playerDistance.magnitude <= firingRange)
            {
                if (currentFireSpeed >= fireSpeedPerSecond)
                {
                    currentFireSpeed = 0;

                    ship.FireRightBroadside();
                }
            }

            if (rightDot <= 0.999)
            {
                if (forwardDot >= 0)
                {
                    ship.RotateLeft();
                }
                else
                {
                    ship.RotateRight();
                }
            }
        }
        else
        {
            if (rightDot <= -0.9 && playerDistance.magnitude <= firingRange)
            {
                if (currentFireSpeed >= fireSpeedPerSecond)
                {
                    currentFireSpeed = 0;

                    ship.FireLeftBroadside();
                }
            }

            if (rightDot >= -0.999)
            {
                if (forwardDot >= 0)
                {
                    ship.RotateRight();
                }
                else
                {
                    ship.RotateLeft();
                }
            }
        }
    }
}