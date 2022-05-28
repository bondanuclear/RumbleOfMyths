using UnityEngine;
using RPG.Saving;
using RPG.Stats;
using RPG.Core;
using System;

namespace RPG.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
       [SerializeField] float health = -1f;
       bool isDead = false;
       public bool IsDead{get{return isDead;}}
        private void Awake() {
            // if bugs with health appears - the reason may be race condition. to fix
            // health = -1f;
             if(health < 0) { health = GetComponent<BaseStats>().GetStat(Stat.Health);}
            // however it may not occur because of Awake method instead of Start method.
            //health = GetComponent<BaseStats>().GetStat(Stat.Health);
        }
        public float GetPercentage()
        {
            return 100 * (health / GetComponent<BaseStats>().GetStat(Stat.Health));
        }
        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
           health = (float) state;
           if(health == 0)
           {
               Debug.Log("Health of " + gameObject.name + " is " + health);
               Death();
           } 
           else if(health > 0)
           {
               //GetComponent<Animator>().SetTrigger("Death");
              // GetComponent<Animator>().SetTrigger("StopDeath");

               isDead = false;
               Debug.Log("This " + gameObject.name + " should be alive ");
           }
           
        }

        public void TakeDamage(GameObject instigator , float damageAmount)
       {
           health = Mathf.Max(health - damageAmount,0);
           if(health <= 0)
            {
                Death();
                ProcessAward(instigator);
            }
        }

        private void ProcessAward(GameObject instigator)
        {
            float experience = GetComponent<BaseStats>().GetStat(Stat.ExperienceReward);
            Experience experienceComponent = instigator.GetComponent<Experience>();
            if(experienceComponent == null) return;
            experienceComponent.GainExperience(experience);
        }


        private void Death()
        {
            if(isDead)
            {
                return;
            }
            //Debug.Log("Dead");
            isDead = true;
            GetComponent<Animator>().SetTrigger("Death");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}
