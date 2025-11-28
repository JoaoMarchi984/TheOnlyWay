using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOW.Core;
using UnityEngine.SceneManagement;

public class EnemyEncounter : MonoBehaviour
{
    [Header("Arraste aqui o prefab do inimigo")]
    public EnemyBattle enemyPrefab;

    private bool playerInside = false;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            GameManager.Instance.enemyToLoad = enemyPrefab;
            GameManager.Instance.lastScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("BaattleScene");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerInside = true;
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
            playerInside = false;
    }
}