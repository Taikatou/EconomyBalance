using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.Crafting
{
    public class CraftingSlider : MonoBehaviour
    {
        public Slider slider;

        public CraftsmanGetAgent CraftsmanGetAgent => GetComponentInParent<CraftsmanGetAgent>();
        public CraftingAbility CraftingAbility => CraftsmanGetAgent.CurrentAgent.CraftingAbility;

        private void Update()
        {
            Time.timeScale = 1.0f;
            slider.value = CraftingAbility.Progress;
        }
    }
}
