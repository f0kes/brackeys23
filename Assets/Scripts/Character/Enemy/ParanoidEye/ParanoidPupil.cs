
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Enemy
{
    public class ParanoidPupil : MonoBehaviour
    {
        [SerializeField] private float shiftDistance;
        
        public Vector2 startPosition;
        
        

        private void Start()
        {
            startPosition = transform.position;
        }

        private void Update()
        {
            var target = Characters.Player.Player.Instance.GetPosition();
            transform.position = startPosition + (target - startPosition).normalized * shiftDistance;
        }
    }
}