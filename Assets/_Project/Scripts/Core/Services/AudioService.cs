using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TOW.Core
{
    public class AudioService
    {
        public void PlayOneShot(AudioClip clip, Vector3 position, float volume = 1f)
        {
            if (clip != null)
                AudioSource.PlayClipAtPoint(clip, position, volume);
        }
    }
}
