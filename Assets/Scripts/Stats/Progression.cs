using UnityEngine;
using System.Collections.Generic;
using System;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "New Progression/Progression", order = 0)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClassArray;
        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;
        [System.Serializable]
        public class ProgressionCharacterClass
        {
            [SerializeField] CharacterClass characterClassType;
            public CharacterClass CharacterClassType { get {return characterClassType;}}
           
            public ProgressionStat[] statArray;
            
            // [SerializeField] float[] health;

            // [SerializeField] float[] damage;
            // public float GetHealthByIndex(int index)
            // {
            //     return health[index];
            // }
        }
        [System.Serializable]
        public class ProgressionStat
        {
            public Stat stat;
            public float[] levels;

        }
        public ProgressionCharacterClass GetProgressionCharacterClass(CharacterClass characterClass)
        {
            for(int i = 0; i < characterClassArray.Length; i++)
            {
                if(characterClassArray[i].CharacterClassType == characterClass)
                {
                    return characterClassArray[i];
                }
            }
            return null;
        }
        public int GetLength(Stat stat, CharacterClass characterClass)
        {
            BuildLookUpTable();
            return lookupTable[characterClass][stat].Length;
        }
        public float GetProgressionStat(Stat myStat, CharacterClass characterClass, int level)
        {
            BuildLookUpTable();
            
            if(!lookupTable.ContainsKey(characterClass)) return 0;
            Dictionary<Stat, float[]> statDictionary = lookupTable[characterClass];
            float[] levelArray = statDictionary[myStat];
           // Debug.Log("Character class: " + characterClass.ToString() + " level" + level);
            return levelArray[level-1];
            
            // for(int i = 0; i < characterClassArray.Length; i++)
            // {
            //     if(characterClassArray[i].CharacterClassType == characterClass)
            //     {
            //         for(int j = 0; j < characterClassArray[i].statArray.Length; j++)
            //         {
            //             if(characterClassArray[i].statArray[j].stat == myStat)
            //             {
            //                 if(characterClassArray[i].statArray[j].levels.Length < level) continue;
            //                 //Debug.Log(characterClassArray[i].statArray[j].levels[level - 1]);
            //                 return characterClassArray[i].statArray[j].levels[level - 1];
            //             }
            //         }
            //         //Debug.Log(characterClassArray[i].GetHealthByIndex(level - 1));
            //        // return characterClassArray[i].GetHealthByIndex(level - 1);
            //     }
            // }
           
        }

        private void BuildLookUpTable()
        {
            if(lookupTable != null) return;
            //List<CharacterClass> characterClasses = new List<CharacterClass>();
            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();
            
            foreach(var character in characterClassArray)
            {
                Dictionary<Stat, float[]> lookupHelper = new Dictionary<Stat, float[]>();
                //Debug.Log("HI");
                //characterClasses.Add(character.CharacterClassType);
                foreach(var stat in character.statArray)
                {
                    //Debug.Log(stat.stat.ToString());
                    lookupHelper[stat.stat] = stat.levels;
                    // Debug.Log(lookupHelper.Keys + " " + lookupHelper.Values);
                   
                    // Debug.Log(lookupTable.Keys);
                    
                }
                // lookupTable.Add(character.CharacterClassType, lookupHelper);
                lookupTable[character.CharacterClassType] = lookupHelper;
            }

        }
    }
}
