using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOW.Core;
using TOW.Data;

namespace TOW.Core

{
    public class GameBootstrapper : MonoBehaviour
    {
        [Header("Scriptable Configs")]
        public GameConfig gameConfig;

        private void Awake()
        {
            // Singleton Services
            ServiceLocator.Register(new RandomService());
            ServiceLocator.Register(new AudioService());
            ServiceLocator.Register(new SaveService());

            // Carrega GameManager (se não existir)
            if (GameManager.Instance == null)
            {
                var go = new GameObject("[GameManager]");
                go.AddComponent<GameManager>();
                DontDestroyOnLoad(go);
            }

            // Aplica configs iniciais
            if (gameConfig != null)
                GameManager.Instance.ApplyConfig(gameConfig);
        }
    }
}

