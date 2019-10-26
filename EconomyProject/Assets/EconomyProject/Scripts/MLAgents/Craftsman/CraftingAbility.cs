using UnityEngine;

namespace Assets.EconomyProject.Scripts.MLAgents.Craftsman
{
    public class CraftingAbility : MonoBehaviour
    {
        public float timeToCreation = 3;

        private float _updateTime = 0.0f;

        private bool _crafting;

        private void Update()
        {
            if (_crafting)
            {
                _updateTime += Time.deltaTime;
                if (_updateTime >= timeToCreation)
                {

                }
            }
        }

        public void SetCraftingItem()
        {
            _crafting = true;
        }

        private void CraftItem()
        {
            _crafting = false;
        }
    }
}
