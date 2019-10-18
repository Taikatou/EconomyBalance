using Assets.EconomyProject.Scripts.GameEconomy.Systems;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI
{
    public class QuestMenu : MonoBehaviour
    {
        public Scrollbar scrollBar;

        public Slider slider;

        private void Update()
        {
            slider.value = Progress;
        }

        public void UpdateText()
        {
            DefaultControls.Resources tempResource = new DefaultControls.Resources();
            GameObject newText = DefaultControls.CreateText(tempResource);

            newText.transform.SetParent(FindContent(scrollBar));
        }

        public RectTransform FindContent(Scrollbar scrollViewObject)
        {
            RectTransform retVal = null;
            Transform[] temp = scrollViewObject.GetComponentsInChildren<Transform>();
            foreach (var child in temp)
            {
                if (child.name == "Content")
                {
                    retVal = child.gameObject.GetComponent<RectTransform>();
                }
            }
            return retVal;
        }

        public float Progress => FindObjectOfType<GameQuests>().Progress;
    }
}
