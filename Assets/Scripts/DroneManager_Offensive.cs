using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneManager_Offensive : DroneManager
{
    public void SetTarget(Transform target)
    {
        foreach(Drone_Offensive iDrone in drones)
        {
            iDrone.target = target;
        }
    }
}