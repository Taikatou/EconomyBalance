using System;
using UnityEngine;

namespace Assets.EconomyProject
{
    public class EndTimerScript : MonoBehaviour
    {
        private float _currentTime;

        private float _startTime;

        public int seconds;

        public int minutes;

        public int hours;

        public string CurrentTime
        {
            get
            {
                TimeSpan t = TimeSpan.FromSeconds(_startTime - _currentTime);

                string currentTimeLeft = $"{t.Hours:D2}h:{t.Minutes:D2}m:{t.Seconds:D2}s:{t.Milliseconds:D3}ms";
                return currentTimeLeft;
            }
        }


        void Start()
        {
            int totalTime = seconds + (60 * minutes) + (60 * 60 * hours);
            _currentTime = totalTime;
            _startTime = totalTime;
        }

        void Update()
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= 0)
            {
                Application.Quit();
            }
        }
    }
}
