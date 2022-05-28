using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Attributes;
using System;

namespace RPG.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
       Health target;
       GameObject player;
       TextMeshProUGUI enemyHealthValue;
       private void Awake() {
           player = GameObject.FindWithTag("Player");
           enemyHealthValue = GetComponent<TextMeshProUGUI>();
       }
        private void Update() {
            target = player.GetComponent<Fighter>().GetTarget();
            if(target == null) return;
            if(target != null)
            enemyHealthValue.text = String.Format("{0:0.0} %", target.GetPercentage()); 
            // 
            
        }
    }
}

