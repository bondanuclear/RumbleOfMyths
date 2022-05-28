using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Stats;
namespace RPG.Attributes
{
    public class LevelDisplay : MonoBehaviour
    {
        TextMeshProUGUI levelText;
        BaseStats baseStats;
        // Start is called before the first frame update
        void Awake()
        {
           
            levelText = GetComponent<TextMeshProUGUI>();
            baseStats = GameObject.FindGameObjectWithTag("Player").GetComponent<BaseStats>();
            
        }

        // Update is called once per frame
        void Update()
        {
            levelText.text = "Level: " + baseStats.CalculateLevel();
        }
    }
}

