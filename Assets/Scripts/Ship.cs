﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ship : MonoBehaviour {

    public List<Transform> leftCannonMouths;
    public List<Transform> rightCannonMouths;

    public Projectile projectileAsset;

    public float velocityPerSecond;
    protected float velocity;
    public float maxVelocity;

    public float rotationPerSecond;

    public int health;

    public ParticleSystem explosionAsset;

    public bool invincible;

    public FiringController firingController;

    public ShipInfo info;

    public Player player;

    protected virtual void Update()
    {
        transform.position += transform.forward * velocity * Time.deltaTime;
    }

    public abstract void SpeedUp();
    public abstract void SlowDown();
    public abstract void RotateLeft();
    public abstract void RotateRight();
    public abstract void TiltLeft();
    public abstract void TiltRight();
    public abstract void TiltForward();
    public abstract void TiltBackward();

    public abstract void Fire();
    public abstract void FireLeftBroadside();
    public abstract void FireRightBroadside();

    public void Damage(int damage)
    {
        if (invincible)
            return;

        health -= damage;

        if(health <= 0)
        {
            Destroy(gameObject);

            ParticleSystem deathExplosion = Instantiate(explosionAsset);
            deathExplosion.transform.position = transform.position;
        }
    }
}