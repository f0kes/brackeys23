
using System.Collections;
using GameState;
using Services.Light;
using Services.Projectile;
using UnityEngine;


namespace Characters.Enemy
{
    public class ParanoidEyeBehaviour : MonoBehaviour, IProjectileHandler
    {
        [SerializeField] private float attackDelay;
        [SerializeField] private float projectileSpeed;
        [SerializeField] private float projectileLifeTime;
        [SerializeField] private int damage;
        [SerializeField] private float deactivatingTime;
        [SerializeField] private float engageDistance;

        [SerializeField] private Projectile projectile;
        [SerializeField] private GameObject openedSprite;
        [SerializeField] private GameObject closedSprite;
        [SerializeField] private ParanoidPupil pupil;
       
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip[] openAudioClips;
        [SerializeField] private AudioClip shotAudioClip;

        private State _state;
        private Coroutine _closeCoroutine;
        private Coroutine _shotCoroutine;

        private void Start()
        {
            Close();
            GameManager.Instance.GetService<ILightService>().OnLightEvent += OnLight;
        }

        private void OnLight(Vector2 position)
        {
            if (Vector2.Distance(transform.position, position) > engageDistance)
            {
                return;
            }


            if (_closeCoroutine != null)
            {
                StopCoroutine(_closeCoroutine);
            }

            _closeCoroutine = StartCoroutine(CloseHandler());
            
            if (_state == State.CLOSED)
            {
                Open();
                _shotCoroutine = StartCoroutine(ShotHandler());
            }
        }


        private IEnumerator CloseHandler()
        {
            yield return new WaitForSeconds(deactivatingTime);
            Close();
            yield return null;
        }

        private IEnumerator ShotHandler()
        {
            while (true)
            {
                yield return new WaitForSeconds(attackDelay);
                Shot();
            }
        }

        private void Shot()
        {
            var centerPosition = pupil.startPosition;
            var target = Characters.Player.Player.Instance.GetPosition();
            var velocity = (target - centerPosition).normalized * projectileSpeed;
            var startPosition = centerPosition + (target - centerPosition).normalized * 0.8f;
            GameManager.Instance.GetService<IProjectileService>().CreateProjectile(projectile, new ProjectileData()
            {
                Range = 999999,
                StartPosition = startPosition,
                Velocity = velocity,
                Lifetime = projectileLifeTime,
                Damage = damage
            }, this); 
            audioSource.PlayOneShot(shotAudioClip);
        }

        private void Close()
        {
            StopAllCoroutines();
            _state = State.CLOSED;
            pupil.gameObject.SetActive(false);
            openedSprite.SetActive(false);
            closedSprite.SetActive(true);
        }

        private void Open()
        {
            _state = State.OPENED;
            pupil.gameObject.SetActive(true);
            openedSprite.SetActive(true);
            closedSprite.SetActive(false);
            audioSource.PlayOneShot(openAudioClips[Random.Range(0, openAudioClips.Length)]);
        }


        private enum State
        {
            CLOSED,
            OPENED,
        }

        public void OnProjectileHit(IProjectile projectile)
        {
        }

        public void OnProjectileTick(IProjectile projectile)
        {
            
        }
    }
}