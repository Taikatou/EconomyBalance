using System;
using Assets.RPG.Scripts.Attributes;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.RPG.Scripts.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        FighterAction _fighterAction;

        private void Awake()
        {
            _fighterAction = GameObject.FindWithTag("Player").GetComponent<FighterAction>();
        }

        private void Update()
        {
            if (_fighterAction.GetTarget() == null)
            {
                GetComponent<Text>().text = "N/A";
                return;
            }
            Health health = _fighterAction.GetTarget();
            GetComponent<Text>().text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}