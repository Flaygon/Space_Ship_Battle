using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FiringController : MonoBehaviour {

    public float reloadTime;
    protected float currentReloadtime;

    private void Start()
    {
        currentReloadtime = reloadTime;
    }

    protected virtual void Update()
    {
        currentReloadtime += Time.deltaTime;
    }

    public abstract void Fire(Transform cannonMouth, Projectile projectileAsset);
    public abstract void Fire(List<Transform> cannonMouths, Projectile projectileAsset);

    protected void FireProjectile(Transform barrelMouth, Projectile projectileAsset)
    {
        Projectile newProjectile = Instantiate(projectileAsset, barrelMouth.position, barrelMouth.rotation);
        newProjectile.Initialize();
    }

    public bool CanFire()
    {
        return currentReloadtime >= reloadTime;
    }
}