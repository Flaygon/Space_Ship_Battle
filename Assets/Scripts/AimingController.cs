using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AimingController : MonoBehaviour
{
    protected Vector3 mouseMovement;
    public float mouseSensitivity;

    public float cameraDistance;

    public Player player;
    public GameObject reticule;

    public bool yUpIsUp = true;
}