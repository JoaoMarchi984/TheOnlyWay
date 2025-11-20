using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomTeleport : MonoBehaviour
{
    [Header("Configuração do Teleporte")]
    public Transform targetSpawn;
    public CinemachineVirtualCamera targetCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Move o Player REAL, não o collider
            var player = other.GetComponentInParent<Player>();
            if (player != null)
            {
                player.transform.position = targetSpawn.position;
            }

            // Troca prioridade das câmeras
            foreach (var cam in FindObjectsOfType<CinemachineVirtualCamera>())
                cam.Priority = 0;

            targetCamera.Priority = 10;
        }
    }
}