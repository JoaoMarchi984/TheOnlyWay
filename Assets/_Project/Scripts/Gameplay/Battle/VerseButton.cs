using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class VerseButton : MonoBehaviour
{
    public Verse verse;
    public BattleSystem battle;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            battle.OnVerseSelected(verse);
        });
    }
}
