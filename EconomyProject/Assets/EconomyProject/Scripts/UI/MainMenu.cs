using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI
{
    enum MenuOpen { Main, Auction, Quest }
    public class MainMenu : MonoBehaviour
    {
        public GameObject auctionMenu;

        public GameObject questMenu;

        public void SwitchToAuction()
        {
            SwitchMenu(MenuOpen.Auction);
        }

        public void SwitchToMain()
        {
            SwitchMenu(MenuOpen.Main);
        }

        public void SwitchToQuest()
        {
            SwitchMenu(MenuOpen.Quest);
        }

        private void SwitchMenu(MenuOpen whichMenu)
        {
            auctionMenu?.SetActive(whichMenu == MenuOpen.Auction);
            gameObject?.SetActive(whichMenu == MenuOpen.Main);
            questMenu?.SetActive(whichMenu == MenuOpen.Quest);
        }
    }
}
