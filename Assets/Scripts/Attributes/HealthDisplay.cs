using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace RPG.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        TextMeshProUGUI healthValue;
        private void Awake() {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            healthValue = GetComponent<TextMeshProUGUI>();
        }

        // Update is called once per frame
        void Update()
        {
            healthValue.text = String.Format("{0:0.0} %",health.GetPercentage());
        }
    }

}