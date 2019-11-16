using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.Craftsman
{
    public class CraftsmanMenu : MonoBehaviour
    {
        public GameObject requestMenu;

        public GameObject craftMenu;

        public GameObject mainMenu;

        public CraftsmanAgent currentAgent;

        private CraftsmanScreen _cachedScreen;

        private void Update()
        {
            var nextScreen = currentAgent.CurrentScreen;
            if (nextScreen != _cachedScreen)
            {
                SwitchMenu(nextScreen);
            }
        }

        public void MoveToRequest()
        {
            currentAgent.ChangeAgentScreen(CraftsmanScreen.Request);
        }

        public void MoveToCraft()
        {
            currentAgent.ChangeAgentScreen(CraftsmanScreen.Craft);
        }

        public void ReturnToMain()
        {
            currentAgent.ChangeAgentScreen(CraftsmanScreen.Main);
        }

        public void SwitchMenu(CraftsmanScreen whichMenu)
        {
            _cachedScreen = currentAgent.CurrentScreen;
            requestMenu?.SetActive(whichMenu == CraftsmanScreen.Request);
            craftMenu.GetComponentInChildren<LastUpdate>()?.Refresh();
            craftMenu?.SetActive(whichMenu == CraftsmanScreen.Craft);
            mainMenu?.SetActive(whichMenu == CraftsmanScreen.Main);
        }
    }
}
