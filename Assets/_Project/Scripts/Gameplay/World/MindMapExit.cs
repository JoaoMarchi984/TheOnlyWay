using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOW.Core;
using UnityEngine.SceneManagement;

public class MindMapExit : MonoBehaviour
{
    private bool canExit = false;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            canExit = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            canExit = false;
    }

    private void Update()
    {
        if (canExit && Input.GetKeyDown(KeyCode.E))
        {
            // Volta para a última cena salva
            SceneManager.LoadScene(GameManager.Instance.lastSceneName);
        }
    }
}
