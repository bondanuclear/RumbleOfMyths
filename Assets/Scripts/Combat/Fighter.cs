using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Attributes;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        Health target;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform leftHandTransform;
        //[SerializeField] AnimatorOverrideController weaponOverride;
        [SerializeField] Weapon defaultWeapon = null;
        //[SerializeField] string weaponName = "Unarmed";
        //Animator animator;
        Weapon currentWeapon = null;
        float timeSinceLastAttack = 0;
        private void Start() {
           // animator  = GetComponent<Animator>();
            // currentWeapon = defaultWeapon;
            //Weapon weapon = Resources.Load<Weapon>(weaponName);
            if(currentWeapon == null)
            EquipWeapon(defaultWeapon);
        }
        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            
             if (target == null) return;
             if (target.IsDead) return;
            if (!IsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);

            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }

        }
        public void EquipWeapon(Weapon weapon)
        {
            //if(weapon == null) return;
            //weaponName = weapon.name;
            Animator animator = GetComponent<Animator>();
            currentWeapon = weapon;
           currentWeapon.Spawn(rightHandTransform, leftHandTransform, animator); 
        }
        public Health GetTarget()
        {
            return target;
        }
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
              
            if(timeSinceLastAttack >= timeBetweenAttacks)
            {
                GetComponent<Animator>().ResetTrigger("StopAttack");
                GetComponent<Animator>().SetTrigger("Attack");
               
                
                timeSinceLastAttack = 0;
            }
            
        }
        void Hit()
        {
            
           if(target == null) return;
            if (currentWeapon.HasProjectile())
            {
                currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target, gameObject);
            }
            else
            target.TakeDamage(gameObject, currentWeapon.Damage);
        }
        void Shoot()
        {
            Hit();
            // if (currentWeapon.HasProjectile())
            // {
            //     currentWeapon.LaunchProjectile(rightHandTransform, leftHandTransform, target);
            // }
        }
        private bool IsInRange()
        {
            return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.Range;
        }
        public bool CanAttack(GameObject target)
        {
            if(target == null) return false;
            if (target.GetComponent<Health>() != null && !target.GetComponent<Health>().IsDead) return true;
            return false;
        }
        public void Attack(GameObject target)
        {
            
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = target.GetComponent<Health>();
        }
        public void Cancel()
        {

            GetComponent<Animator>().SetTrigger("StopAttack");
            GetComponent<Mover>().Cancel();
            target = null;
        }

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
           string weaponName = (string) state;
           Weapon weapon = Resources.Load<Weapon>(weaponName);
           //Debug.Log(weapon.name);
           EquipWeapon(weapon);
        }
    }
}
