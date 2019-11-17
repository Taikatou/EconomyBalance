using UnityEngine;
using UnityEngine.Playables;

namespace Assets.RPG.Scripts.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private bool _alreadyTriggered;

        private void OnTriggerEnter(Component other)
        {
            if (!_alreadyTriggered && other.gameObject.tag == "Player")
            {
                _alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}