using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.Craftsman
{
    public class CraftsmanMenu : GetCurrentAgent<CraftsmanAgent>
    {
        public GameObject requestMenu;

        public GameObject craftMenu;

        public GameObject mainMenu;

        public GameObject agentParent;

        private CraftsmanScreen _cachedScreen;

        public override GameObject AgentParent => agentParent;

        private void Update()
        {
            if (CurrentAgent)
            {
                var nextScreen = CurrentAgent.CurrentScreen;
                if (nextScreen != _cachedScreen)
                {
                    SwitchMenu(nextScreen);
                }
            }
        }

        public void MoveToRequest()
        {
            CurrentAgent.ChangeAgentScreen(CraftsmanScreen.Request);
        }

        public void MoveToCraft()
        {
            CurrentAgent.ChangeAgentScreen(CraftsmanScreen.Craft);
        }

        public void ReturnToMain()
        {
            CurrentAgent.ChangeAgentScreen(CraftsmanScreen.Main);
        }

        public void SwitchMenu(CraftsmanScreen whichMenu)
        {
            _cachedScreen = CurrentAgent.CurrentScreen;
            requestMenu?.SetActive(whichMenu == CraftsmanScreen.Request);
            craftMenu.GetComponentInChildren<LastUpdate>()?.Refresh();
            craftMenu?.SetActive(whichMenu == CraftsmanScreen.Craft);
            mainMenu?.SetActive(whichMenu == CraftsmanScreen.Main);
        }
    }
}
