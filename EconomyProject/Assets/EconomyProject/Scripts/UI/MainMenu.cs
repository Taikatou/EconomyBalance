using Assets.EconomyProject.Scripts.MLAgents;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        public GameObject auctionMenu;

        public GameObject questMenu;

        public void SwitchToAuction()
        {
            SwitchMenu(AgentActionChoice.Auction);
        }

        public void SwitchToMain()
        {
            SwitchMenu(AgentActionChoice.Main);
        }

        public void SwitchToQuest()
        {
            SwitchMenu(AgentActionChoice.Quest);
        }

        public void SwitchMenu(AgentActionChoice whichMenu)
        {
            auctionMenu?.SetActive(whichMenu == AgentActionChoice.Auction);
            gameObject?.SetActive(whichMenu == AgentActionChoice.Main);
            questMenu?.SetActive(whichMenu == AgentActionChoice.Quest);
        }
    }
}
