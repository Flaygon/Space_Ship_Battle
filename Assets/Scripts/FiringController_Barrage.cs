using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiringController_Barrage : FiringController {

    public float barragetime;

    private bool firing;

    public override void Fire(List<Transform> cannonMouths, Projectile projectileAsset)
    {
        if (firing)
            return;

        if (currentReloadtime < reloadTime)
            return;

        firing = true;

        StartCoroutine(Barrage_Routine(cannonMouths, projectileAsset));
    }

    private IEnumerator Barrage_Routine(List<Transform> cannonMouths, Projectile projectileAsset)
    {
        float timeUntilNextFire = barragetime / cannonMouths.Count;

        foreach(Transform iMouth in cannonMouths)
        {
            FireProjectile(iMouth, projectileAsset);

            yield return new WaitForSeconds(timeUntilNextFire);
        }

        currentReloadtime = 0.0f;

        firing = false;
    }
}