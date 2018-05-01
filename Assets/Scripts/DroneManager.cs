using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DroneManager : MonoBehaviour
{
    protected List<Drone> drones = new List<Drone>();

    public int maxDrones;

    public int mouseButton;

    public Drone droneAsset;

    public Ship ship;

    public float betweenDroneTime;
    private float currentBetweenDroneTime = 0.0f;

    private void Update()
    {
        currentBetweenDroneTime += Time.deltaTime;

        if (Input.GetMouseButton(mouseButton))
        {
            if(drones.Count < maxDrones && currentBetweenDroneTime >= betweenDroneTime)
            {
                currentBetweenDroneTime = 0.0f;

                LaunchDrone();
            }
        }
    }

    public void LaunchDrone()
    {
        List<Transform> cannonMouths = Random.Range(0, 2) == 0 ? ship.leftCannonMouths : ship.rightCannonMouths;
        int mouth = Random.Range(0, cannonMouths.Count);
        Drone newDrone = Instantiate(droneAsset, cannonMouths[mouth].position, cannonMouths[mouth].rotation);
        newDrone.manager = this;
        drones.Add(newDrone);
    }

    public void Remove(Drone toRemove)
    {
        drones.Remove(toRemove);
    }
}