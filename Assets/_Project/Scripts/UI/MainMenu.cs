using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NovoJogo()
    {
        SceneManager.LoadScene("MainMap");
    }

    public void Sair()
    {
        Application.Quit();
        Debug.Log("SAINDO DO JOGO...");
    }
}
