using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingController_Sniper : AimingController
{
    private bool lastSniping = false;
    private bool sniping = false;
    private bool snipingRight = true;

    public Vector3 snipingOffset;

    public float fovOriginal;
    public float fovZoom;

    public float snipeMouseSensitivity = 0.1f;

    private void Start()
    {
        Camera.main.fieldOfView = fovOriginal;

        reticule.SetActive(false);
    }

    private void Update()
    {
        sniping = Input.GetMouseButton(1);

        if(sniping && lastSniping != sniping)
        {
            snipingRight = Vector3.Dot(Camera.main.transform.forward, transform.right) >= 0;

            Camera.main.fieldOfView = fovZoom;

            reticule.SetActive(true);
        }
        else if(!sniping && lastSniping != sniping)
        {
            Camera.main.fieldOfView = fovOriginal;

            if(snipingRight)
            {
                mouseMovement.x = transform.eulerAngles.y + 90;
            }
            else
            {
                mouseMovement.x = transform.eulerAngles.y - 90;
            }
            mouseMovement.y = transform.eulerAngles.z;
            // Transform wrapping around quirk
            if(mouseMovement.y > 90)
            {
                mouseMovement.y -= 360;
            }

            reticule.SetActive(false);
        }

        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");
        Vector3 mouseDelta = new Vector3(deltaX, deltaY, 0.0f);
        if (mouseDelta.magnitude >= 0.1)
        {
            mouseMovement += mouseDelta;
            if (sniping)
            {
                float yDelta = (yUpIsUp ? mouseDelta.y : -mouseDelta.y);

                if (!snipingRight)
                {
                    yDelta = -yDelta;
                }

                transform.Rotate(0.0f, mouseDelta.x * mouseSensitivity * snipeMouseSensitivity, yDelta * mouseSensitivity * snipeMouseSensitivity, Space.Self);
            }
            else
            {
                player.transform.Rotate((yUpIsUp ? mouseDelta.y : -mouseDelta.y) * mouseSensitivity, mouseDelta.x * mouseSensitivity, 0.0f, Space.Self);
            }

            mouseMovement.y = Mathf.Clamp(mouseMovement.y, -90.0f, 90.0f);
        }

        lastSniping = sniping;
    }

    private void LateUpdate()
    {
        if(sniping)
        {
            if(snipingRight)
            {
                Camera.main.transform.position = transform.position + transform.TransformVector(snipingOffset);
                Camera.main.transform.rotation = Quaternion.LookRotation(transform.right, transform.up);
            }
            else
            {
                Camera.main.transform.position = transform.position + transform.TransformVector(-snipingOffset);
                Camera.main.transform.rotation = Quaternion.LookRotation(-transform.right, transform.up);
            }
        }
        else
        {
            Camera.main.transform.position = transform.position;
            Camera.main.transform.rotation = Quaternion.identity;
            Camera.main.transform.Rotate(5.0f - mouseMovement.y, mouseMovement.x, 0.0f, Space.Self);
            Camera.main.transform.position -= Camera.main.transform.forward * cameraDistance;
            Camera.main.transform.Rotate(-5.0f, 0.0f, 0.0f, Space.Self);
        }
    }
}