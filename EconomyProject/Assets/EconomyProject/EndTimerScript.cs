using UnityEngine;

namespace Assets.EconomyProject
{
    public class EndTimerScript : MonoBehaviour
    {
        private float _currentTime;

        public int seconds;

        public int minutes;

        public int hours;

        public float CurrentTime => _currentTime;

        void Start()
        {
            int totalTime = seconds + (60 * minutes) + (60 * 60 * hours);
            _currentTime = totalTime;
        }

        // Update is called once per frame
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
