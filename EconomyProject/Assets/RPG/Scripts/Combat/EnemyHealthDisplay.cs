using Assets.RPG.Scripts.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RPG.Scripts.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        private Fighter _fighter;

        private void Awake()
        {
            _fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }

        private void Update()
        {
            if (_fighter.GetTarget() == null)
            {
                GetComponent<Text>().text = "N/A";
                return;
            }
            Health health = _fighter.GetTarget();
            GetComponent<Text>().text = $"{health.GetHealthPoints():0}/{health.GetMaxHealthPoints():0}";
        }
    }
}