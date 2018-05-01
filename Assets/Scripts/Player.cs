using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Ship ship;

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            ship.SpeedUp();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            ship.SlowDown();
        }

        if (Input.GetKey(KeyCode.A))
        {
            ship.RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ship.RotateRight();
        }

        if (Input.GetKey(KeyCode.C))
        {
            ship.TiltBackward();
        }
        else if (Input.GetKey(KeyCode.E))
        {
            ship.TiltForward();
        }

        if(ship.info.continuousFire && Input.GetMouseButton(0) || Input.GetMouseButtonDown(0))
        {
            ship.Fire();
        }
    }
}