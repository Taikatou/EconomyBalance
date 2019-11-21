using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RPG.Scripts.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            GetComponent<Text>().text = $"{health.GetHealthPoints():0}/{health.GetMaxHealthPoints():0}";
        }
    }
}