using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOW.Gameplay.World
{
    public class CameraZone : MonoBehaviour
    {
        [Header("Posição da Câmera neste cômodo")]
        public Transform cameraPosition;

        [Header("Suavidade da transição")]
        public float smoothSpeed = 5f;

        private Camera mainCamera;
        private bool isPlayerInside = false;

        void Start()
        {
            mainCamera = Camera.main;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInside = true;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                isPlayerInside = false;
            }
        }

        void Update()
        {
            if (isPlayerInside && cameraPosition != null)
            {
                Vector3 targetPos = new Vector3(cameraPosition.position.x, cameraPosition.position.y, mainCamera.transform.position.z);
                mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPos, Time.deltaTime * smoothSpeed);
            }
        }
    }
}
