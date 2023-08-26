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
                var damagable = results[i].GetComponent<Player.Player>();
                if (damagable != null)
                {
                    damagable.TakeDamage(Data.Damage);
                    _damageTaken = true;
                }
            }
        }
    }
}