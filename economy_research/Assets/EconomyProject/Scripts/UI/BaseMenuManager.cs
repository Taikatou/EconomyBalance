using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EconomyProject.Scripts.UI
{
    public class OpenedMenu
    {
        public List<GameObject> openedMenus;
        public List<GameObject> closedMenus;

        public OpenedMenu(List<GameObject> openedMenus, List<GameObject> closedMenus)
        {
            this.openedMenus = openedMenus;
            this.closedMenus = closedMenus;
        }

        public void Activate()
        {
            OpenClose(true, openedMenus);
            OpenClose(false, closedMenus);
        }

        public void OpenClose(bool open, List<GameObject> menus)
        {
            foreach (var toOpen in menus)
            {
                toOpen.SetActive(open);
            }
        }
    }
    public abstract class BaseMenuManager <T> : MonoBehaviour
    {
        public T CacheAgentScreen { get; protected set; }

        public abstract Dictionary<T, OpenedMenu> OpenedMenus { get; }

        public abstract bool Compare(T a, T b);

        public virtual void SwitchMenu(T whichMenu)
        {
            var same = Compare(whichMenu, CacheAgentScreen);
            if (!same)
            {
                CacheAgentScreen = whichMenu;
                var openedMenu = OpenedMenus[whichMenu];
                openedMenu.Activate();
            }
        }
    }
}
