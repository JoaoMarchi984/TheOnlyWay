using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TOW.Core;
using TOW.Data;

namespace TOW.Core
{
    public class GameBootstrapper : MonoBehaviour
    {
        public GameManager gameManagerPrefab;

        private void Awake()
        {
            if (GameManager.Instance == null)
            {
                Instantiate(gameManagerPrefab);
            }
        }
    }
}
