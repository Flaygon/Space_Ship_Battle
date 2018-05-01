using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingController_Lockon : AimingController
{
    [HideInInspector]
    public Transform target;

    public GameManager gameManager;

    private void Start()
    {
        reticule.SetActive(false);

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");
        Vector3 mouseDelta = new Vector3(deltaX, deltaY, 0.0f);
        mouseMovement += mouseDelta;
        player.transform.Rotate((yUpIsUp ? mouseDelta.y : -mouseDelta.y) * mouseSensitivity, mouseDelta.x * mouseSensitivity, 0.0f, Space.Self);

        mouseMovement.y = Mathf.Clamp(mouseMovement.y, -90.0f, 90.0f);

        HandleTilt();

        // look for target

        // cull outside view
        List<Transform> candidates = new List<Transform>();
        foreach(Enemy iEnemy in gameManager.enemies)
        {
            if (iEnemy && iEnemy.ship && ContainsPoint(Camera.main.WorldToScreenPoint(iEnemy.ship.transform.position)))
            {
                candidates.Add(iEnemy.ship.transform);
            }
        }

        List<Transform> finalCandidates = new List<Transform>();
        // cull which are visible
        foreach (Transform iCandidate in candidates)
        {
            Vector3 directionToCandidate = (iCandidate.position - transform.position).normalized;
            RaycastHit hit;
            if(Physics.Raycast(transform.position + directionToCandidate * 5.0f, directionToCandidate, out hit, Camera.main.farClipPlane))
            {
                if(hit.collider.transform.parent != null)
                {
                    Ship hitShip = hit.collider.transform.parent.GetComponent<Ship>();
                    if(hitShip)
                    {
                        finalCandidates.Add(iCandidate);
                    }
                }
            }
        }

        // find closest one to screen center and lock on
        Transform closestTarget = null;
        float closestDistance = 99999.0f;
        foreach(Transform iCandidate in finalCandidates)
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(iCandidate.position);
            screenPoint.x -= Screen.width * 0.5f;
            screenPoint.y -= Screen.height * 0.5f;
            screenPoint.z = 0.0f;
            if (screenPoint.magnitude < closestDistance)
            {
                closestDistance = screenPoint.magnitude;
                closestTarget = iCandidate;
            }
        }

        if (closestTarget)
        {
            target = closestTarget;
            reticule.SetActive(true);
            reticule.transform.position = Camera.main.WorldToScreenPoint(closestTarget.position);
        }
        else
        {
            target = null;
            reticule.SetActive(false);
        }
    }

    private bool ContainsPoint(Vector3 screenPoint)
    {
        bool contained = true;
        if(screenPoint.z < 0.0f && screenPoint.z < Camera.main.farClipPlane)
        {
            contained = false;
        }
        if(screenPoint.x < 0 || screenPoint.x > Screen.width)
        {
            contained = false;
        }
        if (screenPoint.y < 0 || screenPoint.y > Screen.height)
        {
            contained = false;
        }
        return contained;
    }

    private void HandleTilt()
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

    private void LateUpdate()
    {
        Camera.main.transform.position = transform.position;
        Camera.main.transform.rotation = Quaternion.identity;
        Camera.main.transform.Rotate(5.0f - mouseMovement.y, mouseMovement.x, 0.0f, Space.Self);
        Camera.main.transform.position -= Camera.main.transform.forward * cameraDistance;
        Camera.main.transform.Rotate(-5.0f, 0.0f, 0.0f, Space.Self);
    }
}