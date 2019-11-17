using UnityEngine;

namespace Assets.RPG.Scripts.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] readonly GameObject _targetToDestroy = null;

        void Update()
        {
            if (!GetComponent<ParticleSystem>().IsAlive())
            {
                if (_targetToDestroy != null)
                {
                    Destroy(_targetToDestroy);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}