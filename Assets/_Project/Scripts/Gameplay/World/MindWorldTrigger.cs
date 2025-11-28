using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOW.Core;
using UnityEngine.SceneManagement;

public class MindMapEnter : MonoBehaviour
{
    [Header("Cena do Mundo da Mente")]
    public string mindSceneName = "Preguica_Phase";

    private bool canEnter = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            canEnter = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            canEnter = false;
    }

    private void Update()
    {
        if (canEnter && Input.GetKeyDown(KeyCode.E))
        {
            // Salva de onde o player veio
            GameManager.Instance.SaveLastScene(
                SceneManager.GetActiveScene().name
            );

            // Vai para o MindMap
            SceneManager.LoadScene(mindSceneName);
        }
    }
}