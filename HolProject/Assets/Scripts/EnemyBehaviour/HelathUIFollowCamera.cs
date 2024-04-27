using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelathUIFollowCamera : MonoBehaviour
{
    Camera playerCamera;
    private void Start()
    {
        playerCamera = GameObject.FindObjectOfType<Camera>();
    }
    private void Update()
    {
        transform.LookAt(playerCamera.transform);
    }
}
