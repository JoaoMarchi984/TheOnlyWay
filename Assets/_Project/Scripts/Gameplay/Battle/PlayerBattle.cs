using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBattle : MonoBehaviour
{
    public int maxFaith = 100;
    public int currentFaith;

    private void Awake()
    {
        currentFaith = maxFaith;
    }

    public void TakeDamage(int amount)
    {
        currentFaith -= amount;
        if (currentFaith < 0) currentFaith = 0;
    }

    public void HealFaith(int amount)
    {
        currentFaith += amount;
        if (currentFaith > maxFaith) currentFaith = maxFaith;
    }
}
