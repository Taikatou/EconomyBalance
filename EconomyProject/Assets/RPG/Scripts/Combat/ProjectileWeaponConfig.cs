using Assets.RPG.Scripts.Attributes;
using UnityEngine;

namespace Assets.RPG.Scripts.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New projectile weapon", order = 0)]
    public class ProjectileWeaponConfig : WeaponConfig
    {
        [SerializeField]
        public Projectile projectile = null;

        public override bool HasProjectile()
        {
            return projectile != null;
        }

        public override void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculatedDamage)
        {
            var projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, instigator, calculatedDamage);
        }
    }
}
