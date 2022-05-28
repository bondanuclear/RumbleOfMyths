using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace RPG.Attributes
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0;
        public float ExperiencePoints {get {return experiencePoints;}}
        
        public event Action onExperienceGained;
        // Start is called before the first frame update
        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }
        public object CaptureState()
        {
            return experiencePoints;
        }
        public void RestoreState(object state)
        {
            experiencePoints = (float) state;
            Debug.Log(experiencePoints);
        }
    }

}