using BuffSystem.Common;
using BuffSystem.Interface;
using UnityEngine.Pool;
using UnityEngine;

namespace WeaponSystem
{
    public class Bullet : MonoBehaviour
    {
        public Weapon ParentWeapon { get; private set; }
        private ObjectPool<Bullet> _pool;
        public Rigidbody2D RB { get; private set; }
        public BoxCollider2D Collider { get; private set; }
        private float traveledDistance;
        private BuffHandler _buffHandler;

        private void Update()
        {
            if (gameObject.activeSelf)
            {
                traveledDistance += Time.deltaTime * ParentWeapon.Data.BulletSpeed;

                if (traveledDistance >= ParentWeapon.Data.BulletTravelDistanceLimit)
                {
                    _pool.Release(this);
                }
            }
        }

        public void Init(Weapon weapon, ObjectPool<Bullet> pool)
        {
            ParentWeapon = weapon;
            _pool = pool;
            RB = GetComponent<Rigidbody2D>();
            Collider = GetComponent<BoxCollider2D>();
            _buffHandler = ParentWeapon.Wielder.GetComponent<IBuffable>().BuffHandler;
        }

        public void Reset()
        {
            traveledDistance = 0;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                _pool.Release(this);
                print("You hit a obstacle");
                return;
            }

            if (other.gameObject.CompareTag("Enemy"))
            {
                float finalDmg =
                    ParentWeapon.Data.Damage * (1 + _buffHandler.WeaponDamageModifier.GetWeaponDmgModValue() +
                                                _buffHandler.FinalDamageModifier.GetFinalDmgModValue());

                other.gameObject.GetComponent<EnemyStats>().TakeDamage(finalDmg);
                _pool.Release(this);
                print("You hit an Enemy");
                return;
            }
        }
    }
}