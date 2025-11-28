using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOW.Data;
using TOW.Core;

public static class CombatMath
{
    public static int CalculatePlayerDamage(int basePower)
    {
        var gm = GameManager.Instance;

        float bonusByVerses = gm.GetCategoryBonus(VerseType.Attack);
        float final = basePower * (1 + bonusByVerses);

        if (gm.jejumActive)
            final *= gm.jejumDamageBonus;

        return Mathf.RoundToInt(final);
    }

    public static int CalculatePlayerHeal(int basePower)
    {
        var gm = GameManager.Instance;

        float bonusByVerses = gm.GetCategoryBonus(VerseType.Heal);
        float percent = (basePower / 100f) * (1 + bonusByVerses);

        return Mathf.RoundToInt(gm.maxFaith * percent);
    }
}
