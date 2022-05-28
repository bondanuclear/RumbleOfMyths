using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Attributes;
namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 90)]
        [SerializeField] int startingLevel = 1;
        
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression;
        int currentLevel = 0;
        public int CurrentLevel {get {return currentLevel;}}
        private void Start() {
            currentLevel = CalculateLevel();
            //Debug.Log(currentLevel);
            Experience experience = GetComponent<Experience>();
            if(experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }
        
        //Instead of calling update every frame we use action (delegates with void return type)
        // Same as with cinematic control remover, we use observer pattern. 
        private void UpdateLevel()
        {
            //print(GetLevel());
            // startingLevel = GetLevel();
            int newLevel = CalculateLevel();
            if(newLevel > currentLevel)
            {
                currentLevel = newLevel;
                Debug.Log("Levelled up!!");
            }
        }
        public float GetStat(Stat stat)
        {
            return progression.GetProgressionStat(stat, characterClass, GetLevel());
        }
        public int GetLevel()
        {
            if(currentLevel != 0)
            return currentLevel;
            else 
            {
                currentLevel = CalculateLevel();
                return currentLevel;
            }    
        }
        public int CalculateLevel()
        {
            if(GetComponent<Experience>() == null) return startingLevel;
           float currentXP = GetComponent<Experience>().ExperiencePoints;

           int extraLevels = progression.GetLength(Stat.ExperienceToLevelUp, characterClass);
           for(int i = 1; i < extraLevels; i++)
           {
               float xpToLvlUp = progression.GetProgressionStat(Stat.ExperienceToLevelUp, characterClass, i);
               if(xpToLvlUp > currentXP)
               {
                   return i;
               }

           } 
           return extraLevels + 1;
           
        }


    }
}

