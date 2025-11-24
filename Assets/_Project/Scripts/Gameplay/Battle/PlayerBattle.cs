using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public int maxFaith = 100;
    public int currentFaith = 100;

    public bool isShielded = false;

    public void TakeDamage(int amount)
    {
        currentFaith -= amount;
        if (currentFaith < 0)
            currentFaith = 0;
    }

    public void Heal(int amount)
    {
        currentFaith += amount;
        if (currentFaith > maxFaith)
            currentFaith = maxFaith;
    }
}