using UnityEngine;

namespace Assets.RPG.Scripts.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] Assets.RPG.Scripts.UI.DamageText.DamageText damageTextPrefab = null;

        public void Spawn(float damageAmount)
        {
            Assets.RPG.Scripts.UI.DamageText.DamageText instance = Instantiate<Assets.RPG.Scripts.UI.DamageText.DamageText>(damageTextPrefab, transform);
            instance.SetValue(damageAmount);
        }
    }
}
