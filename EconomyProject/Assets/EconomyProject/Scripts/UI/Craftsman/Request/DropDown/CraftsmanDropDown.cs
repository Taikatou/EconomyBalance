using System.Collections.Generic;
using Assets.EconomyProject.Scripts.UI.ShopUI.ScrollLists;
using UnityEngine;

namespace Assets.EconomyProject.Scripts.UI.Craftsman.Request.DropDown
{
    public class CraftsmanDropDow<T> : AgentDropDown<T> where T : MonoBehaviour
    {
        public List<LastUpdate> toUpdate;
        protected override void UpdateAgent(T agent)
        {
            foreach (var item in toUpdate)
            {
                item?.Refresh();
            }
        }
    }
}
