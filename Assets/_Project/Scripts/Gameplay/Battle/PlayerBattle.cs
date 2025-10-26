using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOW.Gameplay.Battle
{
    public class PlayerBattle : MonoBehaviour
    {
        [Header("Fé do Jogador")]
        public int maxFaith = 100;
        public int currentFaith;

        void Awake()
        {
            currentFaith = maxFaith;
        }

        public void Setup()
        {
            currentFaith = maxFaith;
            Debug.Log("Player pronto para a batalha com " + currentFaith + " de Fé.");
        }

        public void TakeDamage(int amount)
        {
            currentFaith -= amount;
            if (currentFaith < 0) currentFaith = 0;
            Debug.Log("Player perdeu " + amount + " de Fé! Restante: " + currentFaith);
        }

        public void Heal(int amount)
        {
            currentFaith += amount;
            if (currentFaith > maxFaith) currentFaith = maxFaith;
            Debug.Log("Player recuperou " + amount + " de Fé! Total: " + currentFaith);
        }
    }
}
