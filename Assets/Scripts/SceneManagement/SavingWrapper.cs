using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaultSaveFile = "save";
        [SerializeField] float fadeInTime = 1f;
        public string DefaultSaveFile {get {return defaultSaveFile;}}
        IEnumerator Start() {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediately();
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFile);
            yield return new WaitForSeconds(0.5f);
            yield return fader.FadeIn(fadeInTime);

        }
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
            else if(Input.GetKeyDown(KeyCode.K))
            {
                Save();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFile);
        }
    }

}
