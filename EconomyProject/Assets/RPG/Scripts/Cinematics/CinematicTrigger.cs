using UnityEngine;
using UnityEngine.Playables;

namespace Assets.RPG.Scripts.Cinematics

{
    public class CinematicTrigger : MonoBehaviour
    {
        bool alreadyTriggered = false;
        
        private void OnTriggerEnter(Collider other) 
        {
            if(!alreadyTriggered && other.gameObject.tag == "Player")
            {
                alreadyTriggered = true;
                GetComponent<PlayableDirector>().Play();
            }
        }
    }
}