using Assets.RPG.Scripts.Control;
using Assets.RPG.Scripts.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace Assets.RPG.Scripts.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject _player;

        private void Awake() {
            _player = GameObject.FindWithTag("Player");
        }

        private void OnEnable() {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        private void OnDisable() {
            GetComponent<PlayableDirector>().played -= DisableControl;
            GetComponent<PlayableDirector>().stopped -= EnableControl;
        }

        void DisableControl(PlayableDirector pd)
        {
            _player.GetComponent<ActionScheduler>().CancelCurrentAction();
            _player.GetComponent<PlayerController>().enabled = false;
        }

        void EnableControl(PlayableDirector pd)
        {
            _player.GetComponent<PlayerController>().enabled = true;
        }
    }
}