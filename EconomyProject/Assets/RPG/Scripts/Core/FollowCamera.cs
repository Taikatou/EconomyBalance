using UnityEngine;

namespace Assets.RPG.Scripts.Core
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] Transform _target;

        void LateUpdate()
        {
            transform.position = _target.position;
        }
    }
}