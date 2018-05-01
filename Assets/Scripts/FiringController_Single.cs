using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringController_Single : FiringController {

    public override void Fire(Transform cannonMouth, Projectile projectileAsset)
    {
        if (currentReloadtime < reloadTime)
            return;

        FireProjectile(cannonMouth, projectileAsset);

        currentReloadtime = 0.0f;
    }

    public override void Fire(List<Transform> cannonMouths, Projectile projectileAsset)
    {
        if (currentReloadtime < reloadTime)
            return;

        foreach (Transform iBarrel in cannonMouths)
        {
            FireProjectile(iBarrel, projectileAsset);
        }

        currentReloadtime = 0.0f;
    }
}