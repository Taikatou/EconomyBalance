using Assets.RPG.Scripts.Attributes;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.RPG.Scripts.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField]
        private float _speed = 1;
        [SerializeField]
        private bool isHoming = true;
        [SerializeField]
        private GameObject _hitEffect;
        [SerializeField]
        private float maxLifeTime = 10;
        [SerializeField]
        private GameObject[] _destroyOnHit;
        [SerializeField]
        private float lifeAfterImpact = 2;
        [SerializeField]
        private UnityEvent _onHit;

        Health _target;
        GameObject _instigator;
        float _damage;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        void Update()
        {
            if (_target == null) return;
            if (isHoming && !_target.IsDead())
            {
                transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * _speed * Time.deltaTime);
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this._target = target;
            this._damage = damage;
            this._instigator = instigator;

            Destroy(gameObject, maxLifeTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = _target.GetComponent<CapsuleCollider>();
            if (targetCapsule == null)
            {
                return _target.transform.position;
            }
            return _target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != _target) return;
            if (_target.IsDead()) return;
            _target.TakeDamage(_instigator, _damage);

            _speed = 0;

            _onHit.Invoke();

            if (_hitEffect != null)
            {
                Instantiate(_hitEffect, GetAimLocation(), transform.rotation);
            }

            foreach (GameObject toDestroy in _destroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterImpact);

        }

    }

}