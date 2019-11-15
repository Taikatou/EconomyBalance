using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.Craftsman
{
    public enum CraftsmanScreen { Main, Request, Craft }

    public class CraftsmanMenu : MonoBehaviour
    {
        public GameObject requestMenu;

        public GameObject craftMenu;

        public GameObject mainMenu;

        public void MoveToRequest()
        {
            SwitchMenu(CraftsmanScreen.Request);
        }

        public void MoveToCraft()
        {
            SwitchMenu(CraftsmanScreen.Craft);
        }

        public void ReturnToMain()
        {
            SwitchMenu(CraftsmanScreen.Main);
        }

        public void SwitchMenu(CraftsmanScreen whichMenu)
        {
            requestMenu?.SetActive(whichMenu == CraftsmanScreen.Request);
            craftMenu.GetComponentInChildren<LastUpdate>()?.Refresh();
            craftMenu?.SetActive(whichMenu == CraftsmanScreen.Craft);
            mainMenu?.SetActive(whichMenu == CraftsmanScreen.Main);
        }
    }
}
