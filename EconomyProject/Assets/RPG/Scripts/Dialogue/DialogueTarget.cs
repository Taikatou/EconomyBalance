using Assets.RPG.Scripts.Control;
using UnityEngine;

namespace Assets.RPG.Scripts.Dialogue
{
    public class DialogueTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Dialogue;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            var dialogueAction = callingController.GetComponent<DialogueAction>();
            if (!dialogueAction.CanTalk(gameObject))
            {
                return false;
            }

            if (Input.GetMouseButton(0))
            {
                dialogueAction.StartTalk(gameObject);
            }

            return true;
        }
    }
}
