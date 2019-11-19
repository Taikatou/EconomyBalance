using UnityEngine;
using UnityEngine.UI;

namespace Assets.RPG.Scripts.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Experience _experience;

        private void Awake()
        {
            _experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            GetComponent<Text>().text = $"{_experience.GetPoints():0}";
        }
    }
}