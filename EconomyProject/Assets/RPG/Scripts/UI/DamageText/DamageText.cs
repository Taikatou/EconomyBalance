using UnityEngine;
using UnityEngine.UI;

namespace Assets.RPG.Scripts.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField]
        private readonly Text _damageText = null;

        public void DestroyText()
        {
            Destroy(gameObject);
        }

        public void SetValue(float amount)
        {
            _damageText.text = $"{amount:0}";
        }
    }
}