using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomTeleport : MonoBehaviour
{
    public Transform targetSpawn;
    public CinemachineVirtualCamera targetCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleporta player
            other.transform.position = targetSpawn.position;

            // Desativa todas as câmeras
            foreach (var cam in FindObjectsOfType<CinemachineVirtualCamera>())
                cam.Priority = 0;

            // Ativa a câmera do cômodo atual
            targetCamera.Priority = 10;
        }
    }
}