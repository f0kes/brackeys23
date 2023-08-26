using Characters.Player;
using UnityEngine;

namespace Progression.MemoryItem
{
    public class RadioItem : MonoBehaviour
    {
        [SerializeField] private float activationRange = 2f;
        [SerializeField] private AudioClip audioClip;

        private void Update()
        {
            var player = Player.Instance;
            if(player == null) return;
            var distance = Vector2.Distance(transform.position, player.transform.position);
            if(!(distance < activationRange)) return;
            player.GetAudioSource().PlayOneShot(audioClip);
            gameObject.SetActive(false);
        }
    }
}