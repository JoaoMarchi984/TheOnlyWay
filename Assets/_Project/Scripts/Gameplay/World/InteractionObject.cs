using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOW.Core;
using UnityEngine.SceneManagement;

public class InteractionObject : MonoBehaviour
{
    [Header("ID único do inimigo ou tentação")]
    public string enemyID = "default";

    [Header("Batalha que será carregada")]
    public string battleSceneName = "BaattleScene"; // nome exato da tua cena de batalha

    private bool playerInside = false;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            // Salva de onde o player veio (MainMap ou Preguica_Phase)
            GameManager.Instance.lastSceneBeforeVerses =
                SceneManager.GetActiveScene().name;

            // Salva qual inimigo deve ser carregado
            GameManager.Instance.currentEnemyID = enemyID;

            // Vai pra luta
            SceneManager.LoadScene(battleSceneName);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            playerInside = false;
    }
}
