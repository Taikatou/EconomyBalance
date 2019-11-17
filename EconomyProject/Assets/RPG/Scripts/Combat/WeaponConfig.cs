using Assets.EconomyProject.Scripts.Inventory;
using Assets.RPG.Scripts.Attributes;
using UnityEngine;

namespace Assets.RPG.Scripts.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class WeaponConfig : InventoryItem
    {
        [SerializeField]
            public AnimatorOverrideController animatorOverride = null;
        [SerializeField]
            public Weapon equippedPrefab = null;
        [SerializeField]
            public float weaponDamage = 5f;
        [SerializeField]
            public float percentageBonus = 0;
        [SerializeField]
            public float weaponRange = 2f;
        [SerializeField]
            public bool isRightHanded = true;


        const string WeaponName = "Weapon";

        public Weapon Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            Weapon weapon = null;

            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                weapon = Instantiate(equippedPrefab, handTransform);
                weapon.gameObject.name = WeaponName;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride; 
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }

            return weapon;
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            var oldWeapon = rightHand.Find(WeaponName);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(WeaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "DESTROYING";
            Destroy(oldWeapon.gameObject);
        }

        protected Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            return isRightHanded? rightHand : leftHand;
        }

        public virtual bool HasProjectile()
        {
            return false;
        }

        public virtual void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            
        }

        public float GetDamage()
        {
            return weaponDamage;
        }

        public float GetPercentageBonus()
        {
            return percentageBonus;
        }

        public float GetRange()
        {
            return weaponRange;
        }
    }
}
