using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingController_PlanetaryCannon : AimingController
{
    private bool lastSniping = false;
    private bool sniping = false;

    public Vector3 snipingOffset;

    public float fovOriginal;
    public float fovZoom;

    public float snipeMouseSensitivity = 0.1f;

    public Vector2 attachedConstraints;

    private void Start()
    {
        Camera.main.fieldOfView = fovOriginal;

        reticule.SetActive(false);
    }

    private void Update()
    {
        sniping = !GetComponent<Ship_PlanetaryCannon>().detached;

        if(sniping && lastSniping != sniping)
        {
            Camera.main.fieldOfView = fovZoom;

            reticule.SetActive(true);
        }
        else if(!sniping && lastSniping != sniping)
        {
            Camera.main.fieldOfView = fovOriginal;

            mouseMovement.x = transform.eulerAngles.y;
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
        mouseMovement += mouseDelta;
        if (sniping)
        {
            float yDelta = (yUpIsUp ? -mouseDelta.y : mouseDelta.y);
            transform.Rotate(yDelta * mouseSensitivity * snipeMouseSensitivity, mouseDelta.x * mouseSensitivity * snipeMouseSensitivity, 0.0f, Space.Self);
            float baseX = transform.eulerAngles.y;
            float baseY = transform.eulerAngles.z;
            mouseMovement.x = Mathf.Clamp(mouseMovement.x, baseX - attachedConstraints.x, baseX + attachedConstraints.x);
            mouseMovement.y = Mathf.Clamp(mouseMovement.y, baseY - attachedConstraints.y, baseY + attachedConstraints.y);
        }
        else
        {
            transform.Rotate((yUpIsUp ? mouseDelta.y : -mouseDelta.y) * mouseSensitivity, mouseDelta.x * mouseSensitivity, 0.0f, Space.Self);
            mouseMovement.y = Mathf.Clamp(mouseMovement.y, -90.0f, 90.0f);
        }

        lastSniping = sniping;
    }

    private void LateUpdate()
    {
        if(sniping)
        {
            Camera.main.transform.position = transform.position + transform.TransformVector(snipingOffset);
            Camera.main.transform.rotation = Quaternion.LookRotation(transform.forward, transform.up);
        }
        else
        {
            Camera.main.transform.position = transform.position;
            Camera.main.transform.rotation = Quaternion.LookRotation(-transform.forward.normalized, transform.up.normalized);
            Camera.main.transform.Rotate(5.0f, 0.0f, 0.0f, Space.Self);
            Camera.main.transform.position -= Camera.main.transform.forward * cameraDistance;
            Camera.main.transform.Rotate(-5.0f, 0.0f, 0.0f, Space.Self);
        }
    }
}