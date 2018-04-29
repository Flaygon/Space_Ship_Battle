using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingController_Simple : AimingController
{
    private void Update()
    {
        Vector3 mouseDelta = Input.mousePosition - lastMousePosition;
        if (mouseDelta.magnitude >= 0.1)
        {
            mouseMovement += mouseDelta;
            transform.Rotate((yUpIsUp ? mouseDelta.y : -mouseDelta.y) * mouseSensitivity, mouseDelta.x * mouseSensitivity, 0.0f, Space.Self);

            mouseMovement.y = Mathf.Clamp(mouseMovement.y, -90.0f, 90.0f);
            lastMousePosition = Input.mousePosition;
        }

        HandleTilt();
    }

    private void HandleTilt()
    {
        if (player.ship.info.autoTilt || Input.GetMouseButton(1))
        {
            float upCheck = Vector3.Dot(Camera.main.transform.forward, player.ship.transform.up);
            float rightCheck = Vector3.Dot(Camera.main.transform.forward, player.ship.transform.right);
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
        Camera.main.transform.position = player.ship.transform.position;
        Camera.main.transform.rotation = Quaternion.identity;
        Camera.main.transform.Rotate(5.0f - mouseMovement.y, mouseMovement.x, 0.0f, Space.Self);
        Camera.main.transform.position -= Camera.main.transform.forward * cameraDistance;
        Camera.main.transform.Rotate(-5.0f, 0.0f, 0.0f, Space.Self);
    }
}