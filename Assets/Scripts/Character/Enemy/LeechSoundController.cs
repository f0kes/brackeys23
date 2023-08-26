using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters.Enemy
{
    public class LeechSoundController : MonoBehaviour
    {
        [SerializeField] private Character character;
        [SerializeField] private AudioClip[] spawnAudioClips;
        
        private void Start()
        {
            character.GetAudioSource().PlayOneShot(spawnAudioClips[Random.Range(0, spawnAudioClips.Length)]);
        }
    }
}