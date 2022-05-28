using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }
        [SerializeField] int sceneToLoad = -1;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 2.5f;
        [SerializeField] float fadeInTime = 2f;
        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player"))
            {
                StartCoroutine(Transition());
            }
        }
        
        private IEnumerator Transition()
        {
            Fader fader = FindObjectOfType<Fader>();
            GameObject.DontDestroyOnLoad(this.gameObject);
            
            
            yield return fader.FadeOut(fadeOutTime);
            // save
            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
           // load
            savingWrapper.Load();
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer(otherPortal);
            savingWrapper.Save();
            yield return fader.FadeIn(fadeInTime);
            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            
            //player.transform.position = otherPortal.spawnPoint.transform.position;
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.spawnPoint.transform.position);
        }

        private Portal GetOtherPortal()
        {
            Portal[] portals = FindObjectsOfType<Portal>();
            Debug.Log(portals.Length);
            foreach(Portal portal in portals)
            {
                if(portal==this) continue;
                else if(this.destination == portal.destination)
                {
                    Debug.Log($"{this.destination} and {portal.destination}");
                    return portal;
                } 
            }

           return null;
        }
        
    }
}

