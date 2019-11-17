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
            throw new System.NotImplementedException();
        }
    }
}
