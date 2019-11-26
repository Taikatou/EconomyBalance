using System.Collections.Generic;
using Assets.EconomyProject.Scripts.MLAgents.Craftsman;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.Craftsman
{
    public class CraftsmanMenu : BaseMenuManager<CraftsmanScreen>
    {
        public GameObject requestMenu;

        public GameObject craftMenu;

        public GameObject mainMenu;

        public CraftsmanUIControls CraftsmanUiControls => GetComponent<CraftsmanUIControls>();

        private void Update()
        {
            if (CraftsmanUiControls.CraftsmanAgent)
            {
                var nextScreen = CraftsmanUiControls.CraftsmanAgent.CurrentScreen;
                SwitchMenu(nextScreen);
            }
        }

        public override Dictionary<CraftsmanScreen, OpenedMenu> OpenedMenus => new Dictionary<CraftsmanScreen, OpenedMenu>
        {
            { CraftsmanScreen.Main, new OpenedMenu(new List<GameObject>{mainMenu}, new List<GameObject>{ craftMenu, requestMenu}) },
            { CraftsmanScreen.Craft, new OpenedMenu(new List<GameObject>{craftMenu}, new List<GameObject>{ requestMenu, mainMenu}) },
            { CraftsmanScreen.Request, new OpenedMenu(new List<GameObject>{requestMenu}, new List<GameObject>{ mainMenu, craftMenu }) }
        };

        public override bool Compare(CraftsmanScreen a, CraftsmanScreen b)
        {
            return a == b;
        }

        public override void SwitchMenu(CraftsmanScreen whichMenu)
        {
            base.SwitchMenu(whichMenu);
            craftMenu.GetComponentInChildren<LastUpdate>()?.Refresh();
        }
    }
}
