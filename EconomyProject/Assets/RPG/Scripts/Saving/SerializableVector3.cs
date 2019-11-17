using UnityEngine;

namespace Assets.RPG.Scripts.Saving
{
    [System.Serializable]
    public class SerializableVector3
    {
        private readonly float _x;
        private readonly float _y;
        private readonly float _z;

        public SerializableVector3(Vector3 vector)
        {
            _x = vector.x;
            _y = vector.y;
            _z = vector.z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(_x, _y, _z);
        }
    }
}