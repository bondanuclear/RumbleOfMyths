using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
namespace RPG.Attributes
{
    public class ExperienceDisplay : MonoBehaviour
    {
        TextMeshProUGUI experienceText;
        Experience experience;
        // Start is called before the first frame update
        void Awake()
        {
            experienceText = GetComponent<TextMeshProUGUI>();
            experience = GameObject.FindGameObjectWithTag("Player").GetComponent<Experience>();
        }

        // Update is called once per frame
        void Update()
        {
            experienceText.text = "Experience: " + experience.ExperiencePoints;
        }
    }

}
