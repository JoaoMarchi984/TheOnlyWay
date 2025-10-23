using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOW.Core
{
    public class RandomService
    {
        public int Range(int min, int max)
        {
            return Random.Range(min, max);
        }

        public float Range(float min, float max)
        {
            return Random.Range(min, max);
        }
    }
}
