using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public enum VerseType { Attack, Heal, Shield }

[System.Serializable]
public class Verse
{
    public string verseName;
    public VerseType type;
}

public class BattleSystem : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI playerFaithText;
    public TextMeshProUGUI enemyHealthText;
    public TextMeshProUGUI logText;

    [Header("Buttons")]
    public Button[] verseButtons;
    public Verse[] verses;

    [Header("Stats")]
    public int playerFaith = 100;
    public int enemyHealth = 100;
    private int playerShield = 0;

    private bool playerCanAct = true;

    void Start()
    {
        AssignButtonNames();  // coloca nome no botão
        HideAllButtons();
        Invoke(nameof(ShowAllButtons), 0.5f);

        UpdateUI();
        logText.text = "Seu turno! Escolha um versículo.";
    }

    // ================================
    // POLIMENTO
    // ================================

    void AssignButtonNames()
    {
        for (int i = 0; i < verseButtons.Length; i++)
        {
            if (i < verses.Length)
            {
                TextMeshProUGUI t = verseButtons[i].GetComponentInChildren<TextMeshProUGUI>();
                t.text = verses[i].verseName;

                int index = i;
                verseButtons[i].onClick.AddListener(() => OnVerseSelected(verses[index]));
            }
        }
    }

    void HideAllButtons()
    {
        foreach (var b in verseButtons)
            b.gameObject.SetActive(false);
    }

    void ShowAllButtons()
    {
        foreach (var b in verseButtons)
            b.gameObject.SetActive(true);
    }

    // ================================
    // LÓGICA PRINCIPAL
    // ================================
    public void OnVerseSelected(Verse verse)
    {
        if (!playerCanAct) return; // anti-spam

        playerCanAct = false;
        HideAllButtons();

        switch (verse.type)
        {
            case VerseType.Attack:
                enemyHealth -= 20;
                logText.text = $"{verse.verseName}: Você atacou com poder espiritual!";
                break;

            case VerseType.Heal:
                playerFaith += 15;
                if (playerFaith > 100) playerFaith = 100;
                logText.text = $"{verse.verseName}: Você restaurou sua fé!";
                break;

            case VerseType.Shield:
                playerShield = 15;
                logText.text = $"{verse.verseName}: Sua resistência espiritual aumentou!";
                break;
        }

        UpdateUI();

        // Inimigo age depois de 1 segundo
        Invoke(nameof(EnemyTurn), 1.0f);
    }

    void EnemyTurn()
    {
        int damage = 20;

        if (playerShield > 0)
        {
            damage -= playerShield;
            if (damage < 0) damage = 0;
            playerShield = 0;
        }

        playerFaith -= damage;
        if (playerFaith < 0) playerFaith = 0;

        logText.text += $"\nA tentação atacou causando {damage} de dano!";

        UpdateUI();

        // volta turno do player após delay
        Invoke(nameof(EnablePlayerTurn), 1.0f);
    }

    void EnablePlayerTurn()
    {
        playerCanAct = true;
        ShowAllButtons();
        logText.text = "Seu turno!";
    }

    void UpdateUI()
    {
        playerFaithText.text = $"Fé: {playerFaith}/100";
        enemyHealthText.text = $"Vida: {enemyHealth}/100";
    }
}
