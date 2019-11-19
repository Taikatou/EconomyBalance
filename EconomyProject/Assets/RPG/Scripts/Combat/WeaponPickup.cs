using Assets.RPG.Scripts.Attributes;
using Assets.RPG.Scripts.Control;
using System.Collections;
using UnityEngine;

namespace Assets.RPG.Scripts.Combat
{
    public class WeaponPickup : MonoBehaviour, IRaycastable
    {
        [SerializeField]
        private readonly WeaponConfig _weapon = null;
        [SerializeField]
        private float healthToRestore = 0;
        [SerializeField]
        private readonly float _respawnTime = 5;

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Pickup(other.gameObject);
            }
        }

        private void Pickup(GameObject subject)
        {
            if (_weapon != null)
            {
                subject.GetComponent<Fighter>().EquipWeapon(_weapon);
            }
            if (healthToRestore > 0)
            {
                subject.GetComponent<Health>().Heal(healthToRestore);
            }
            StartCoroutine(HideForSeconds(_respawnTime));
        }

        private IEnumerator HideForSeconds(float seconds)
        {
            ShowPickup(false);
            yield return new WaitForSeconds(seconds);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<Collider>().enabled = shouldShow;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(shouldShow);
            }
        }

        public bool HandleRaycast(PlayerController callingController)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Pickup(callingController.gameObject);
            }
            return true;
        }

        public CursorType GetCursorType()
        {
            return CursorType.Pickup;
        }
    }
}