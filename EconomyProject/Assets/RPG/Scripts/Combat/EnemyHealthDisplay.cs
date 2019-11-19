using System;
using Assets.RPG.Scripts.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RPG.Scripts.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter _fighter;

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
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}