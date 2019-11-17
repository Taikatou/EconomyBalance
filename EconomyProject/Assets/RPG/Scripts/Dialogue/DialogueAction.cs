using Assets.RPG.Scripts.Attributes;
using Assets.RPG.Scripts.Core;
using Assets.RPG.Scripts.Movement;
using UnityEngine;
using Yarn.Unity;
using Yarn.Unity.Example;

namespace Assets.RPG.Scripts.Dialogue
{
    public class DialogueAction : MonoBehaviour, IAction
    {
        private NPC _target;
        private bool _talking;
        private bool _startTalking;

        public  DialogueRunner DialogueRunner => FindObjectOfType<DialogueRunner>();
        public void StartTalk(GameObject talkTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);

            _target = talkTarget.GetComponent<NPC>();

            _startTalking = true;
        }

        public void Talk()
        {
            if (!_talking)
            {
                _talking = true;
                DialogueRunner.StartDialogue(_target.talkToNode);
            }
        }

        public void Cancel()
        {
            _talking = false;
            _startTalking = false;
            DialogueRunner.Stop();
        }

        private void Update()
        {
            if (_startTalking)
            {
                if (!GetIsInRange())
                {
                    GetComponent<Mover>().MoveTo(_target.transform.position, 1f);
                }
                else
                {
                    GetComponent<Mover>().Cancel();
                    Talk();
                }
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, _target.transform.position) < 3;
        }

        public bool CanTalk(GameObject dialogueTarget)
        {
            if (dialogueTarget == null)
            {
                return false;
            }

            if (!GetComponent<Mover>().CanMoveTo(dialogueTarget.transform.position))
            {
                return false;
            }

            var targetToTest = dialogueTarget.GetComponent<Health>();
            if (targetToTest)
            {
                return !targetToTest.IsDead();
            }

            return true;
        }
    }
}
