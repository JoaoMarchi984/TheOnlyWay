using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TeleportSwitchCam : MonoBehaviour
{
    [Header("Destino")]
    public Transform targetSpawn;

    [Header("Câmeras")]
    public CinemachineVirtualCamera cameraToActivate;
    public CinemachineVirtualCamera cameraToDisable;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;

        if (rb == null) return;

        // Teleporta player
        rb.position = targetSpawn.position;

        // Troca câmeras
        if (cameraToActivate != null)
            cameraToActivate.Priority = 20;

        if (cameraToDisable != null)
            cameraToDisable.Priority = 5;
    }
}