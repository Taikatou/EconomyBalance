using UnityEngine;

namespace Assets.RPG.Scripts.Core
{
    public class PeristentObjectSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _persistentObjectPrefab;

        private static bool _hasSpawned;

        private void Awake()
        {
            if (_hasSpawned) return;

            SpawnPersistentObjects();

            _hasSpawned = true;
        }

        private void SpawnPersistentObjects()
        {
            GameObject persistentObject = Instantiate(_persistentObjectPrefab);
            DontDestroyOnLoad(persistentObject);
        }
    }
}