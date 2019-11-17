using UnityEngine;
using UnityEngine.UI;

namespace Assets.RPG.Scripts.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            _health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            GetComponent<Text>().text = $"{_health.GetHealthPoints():0}/{_health.GetMaxHealthPoints():0}";
        }
    }
}