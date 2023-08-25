using Services.Projectile;
using UnityEngine;

namespace Characters.Enemy
{
    public class ParanoidEyeProjectile : Projectile
    {
        private bool _damageTaken = false;
        public override void Launch(ProjectileData data)
        {
            Data = data;
            transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(Data.Velocity.y, Data.Velocity.x) * Mathf.Rad2Deg);
            _launched = true;
        }


        private void OnCollisionEnter2D(Collision2D col)
        {
            Debug.Log(1);
            col.gameObject.TryGetComponent(out ParanoidEyeBehaviour eye);
            if (eye != null)
            {
                return;
            }
            
            col.gameObject.TryGetComponent(out Player.Player player);
            if (player != null)
            {
                Debug.Log(2);
                player.TakeDamage(Data.Damage);
            }
            Rigidbody.velocity = Vector2.zero;
            Destroy(this.gameObject, 1f);
            _launched = false;
            
            
        }

        protected override void Move()
        {
            base.Move();
            if (_damageTaken)
            {
                return;
            }
            var results = new Collider2D[10];
            var size = Physics2D.OverlapCircleNonAlloc(Rigidbody.position, 0.22f, results);

            for (var i = 0; i < size; i++)
            {
                var damagable = results[i].GetComponent<IDamagable>();
                if (damagable != null)
                {
                    damagable.TakeDamage(Data.Damage);
                    _damageTaken = true;
                }
            }
        }
    }
}