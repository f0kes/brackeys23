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
            col.gameObject.TryGetComponent(out ParanoidEyeBehaviour eye);
            if (eye != null)
            {
                return;
            }
            if (_damageTaken)
            {
                return;
            }
            
            col.gameObject.TryGetComponent(out Player.Player player);
            if (player != null)
            {
                player.TakeDamage(Data.Damage);
            }
            Rigidbody.velocity = Vector2.zero;
            Destroy(this.gameObject, 1f);
            _launched = false;
            _damageTaken = true;
            
        }
    }
}