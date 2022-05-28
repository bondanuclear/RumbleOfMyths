using System;
using RPG.Attributes;

using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Make a Weapon/Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject weaponPrefab;
        [SerializeField] AnimatorOverrideController weaponAnimatorOverride;
        [SerializeField] float range;
        [SerializeField] float damage;
        [SerializeField] bool isRightHanded = true;
        [SerializeField] Projectile projectile = null;
        public float Damage{ get { return damage; } } 
        public float Range {get {return range;}}

        const string weaponName = "Weapon";
        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);
            if(weaponPrefab != null)
            {
                Transform handTransform = GetTransform(rightHand, leftHand);
                GameObject weapon = Instantiate(weaponPrefab, handTransform);
                weapon.name = weaponName;
            }
                
            if (weaponAnimatorOverride != null)
            {
                 //Debug.Log(animator.runtimeAnimatorController);
                // Debug.Log($"weapon override {weaponAnimatorOverride.name}");
                 animator.runtimeAnimatorController = weaponAnimatorOverride;
            }
             else{
                var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
                
                if(overrideController != null)
                {
                    animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
                }
            }
        }

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(weaponName);
            if(oldWeapon == null)
            {
                oldWeapon = leftHand.Find(weaponName);
            }
            if(oldWeapon == null) return;
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHand, Transform leftHand)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            return handTransform;
        }
        public bool HasProjectile()
        {
            return projectile != null;
        }
        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity );
            projectileInstance.SetTarget(target, damage, instigator);
            
        }
    }
}