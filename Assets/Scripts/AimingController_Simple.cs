using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingController_Simple : AimingController
{

    private void Start()
    {
        reticule.SetActive(true);
    }

    private void Update()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");
        Vector3 mouseDelta = new Vector3(deltaX, deltaY, 0.0f);
        if (mouseDelta.magnitude >= 0.1)
        {
            mouseMovement += mouseDelta;
            player.transform.Rotate((yUpIsUp ? mouseDelta.y : -mouseDelta.y) * mouseSensitivity, mouseDelta.x * mouseSensitivity, 0.0f, Space.Self);

            mouseMovement.y = Mathf.Clamp(mouseMovement.y, -90.0f, 90.0f);
        }

        HandleTilt();
    }

    private void HandleTilt()
    {
        if (player.ship.info.autoTilt || Input.GetMouseButton(1))
        {
            float upCheck = Vector3.Dot(Camera.main.transform.forward, transform.up);
            float rightCheck = Vector3.Dot(Camera.main.transform.forward, transform.right);
            if (upCheck > 0.001)
            {
                if (rightCheck > 0.001)
                {
                    player.ship.TiltLeft();
                }
                else if (rightCheck < -0.001)
                {
                    player.ship.TiltRight();
                }
            }
            else if (upCheck < -0.001)
            {
                if (rightCheck > 0.001)
                {
                    player.ship.TiltRight();
                }
                else if (rightCheck < -0.001)
                {
                    player.ship.TiltLeft();
                }
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Q))
            {
                player.ship.TiltLeft();
            }
            else if (Input.GetKey(KeyCode.Z))
            {
                player.ship.TiltRight();
            }
        }
    }

    private void LateUpdate()
    {
        Camera.main.transform.position = transform.position;
        Camera.main.transform.rotation = Quaternion.identity;
        Camera.main.transform.Rotate(5.0f - mouseMovement.y, mouseMovement.x, 0.0f, Space.Self);
        Camera.main.transform.position -= Camera.main.transform.forward * cameraDistance;
        Camera.main.transform.Rotate(-5.0f, 0.0f, 0.0f, Space.Self);
    }
}