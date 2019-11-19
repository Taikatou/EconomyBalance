using UnityEngine;
using UnityEngine.Events;

namespace Assets.RPG.Scripts.Combat
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] UnityEvent _onHit;

        public void OnHit()
        {
            _onHit.Invoke();
        }
    }
}