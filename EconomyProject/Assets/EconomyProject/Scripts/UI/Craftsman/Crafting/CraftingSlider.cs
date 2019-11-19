using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.Crafting
{
    public class CraftingSlider : MonoBehaviour
    {
        public Slider slider;

        public CraftingAbility craftingAbility;

        private void Update()
        {
            Time.timeScale = 1.0f;
            slider.value = craftingAbility.Progress;
        }
    }
}
