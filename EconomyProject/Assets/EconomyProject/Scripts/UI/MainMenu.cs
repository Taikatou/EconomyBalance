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
            SwitchMenu(AgentScreen.Auction);
        }

        public void SwitchToMain()
        {
            SwitchMenu(AgentScreen.Main);
        }

        public void SwitchToQuest()
        {
            SwitchMenu(AgentScreen.Quest);
        }

        public void SwitchMenu(AgentScreen whichMenu)
        {
            Debug.Log("Choosen screen " + whichMenu.ToString());
            auctionMenu?.SetActive(whichMenu == AgentScreen.Auction);
            gameObject?.SetActive(whichMenu == AgentScreen.Main);
            questMenu?.SetActive(whichMenu == AgentScreen.Quest);
        }
    }
}
