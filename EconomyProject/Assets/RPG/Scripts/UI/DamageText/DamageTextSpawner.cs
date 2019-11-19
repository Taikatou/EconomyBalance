using UnityEngine;

namespace Assets.RPG.Scripts.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] readonly DamageText _damageTextPrefab = null;

        public void Spawn(float damageAmount)
        {
            var instance = Instantiate<DamageText>(_damageTextPrefab, transform);
            instance.SetValue(damageAmount);
        }
    }
}
