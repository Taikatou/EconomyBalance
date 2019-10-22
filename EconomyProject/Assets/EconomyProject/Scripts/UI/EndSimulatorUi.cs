using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.EconomyProject.Scripts.UI
{
    public class EndSimulatorUi : MonoBehaviour
    {
        public Text auctionText;

        public EndTimerScript endTimer;
        void Update()
        {
            Debug.Log(endTimer.CurrentTime);
            TimeSpan t = TimeSpan.FromSeconds(endTimer.CurrentTime);

            string currentTimeLeft = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s:{t.Milliseconds:D3}ms";

            auctionText.text = "REMAINING: " + currentTimeLeft;
        }
    }
}
