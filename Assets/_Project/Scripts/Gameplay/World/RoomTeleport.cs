using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomTeleport : MonoBehaviour
{
    [Header("Configuração do Teleporte")]
    public Transform targetSpawn; // Ponto de destino
    public CinemachineVirtualCamera targetCamera; // Câmera do novo cômodo

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Move o player
            other.transform.position = targetSpawn.position;

            // Troca a prioridade das câmeras
            foreach (var cam in FindObjectsOfType<CinemachineVirtualCamera>())
                cam.Priority = 0;

            targetCamera.Priority = 10;
        }
    }
}
