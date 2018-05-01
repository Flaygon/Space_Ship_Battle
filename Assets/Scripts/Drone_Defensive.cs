using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone_Defensive : Drone
{
    protected override void Update()
    {
        HandleOrbital(manager.transform);
    }
}