using UnityEngine;
using UnityEngine.Playables;

namespace Assets.RPG.Scripts.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        bool _alreadyTriggered = false;
        
        private void OnTriggerEnter(Collider other) 
        {
            if(!_alreadyTriggered && other.gameObject.tag == "Player")
            {
                _alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}