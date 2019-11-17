using Assets.RPG.Scripts.Attributes;
using Assets.RPG.Scripts.Control;
using UnityEngine;

namespace Assets.RPG.Scripts.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public CursorType GetCursorType()
        {
            return CursorType.Combat;
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            FighterAction fighterAction = callingController.GetComponent<FighterAction>();
            if (!fighterAction.CanAttack(gameObject))
            {
                return false;
            }

            if (Input.GetMouseButton(0))
            {
                fighterAction.Attack(gameObject);
            }

            return true;
        }
    }
}