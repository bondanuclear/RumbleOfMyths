using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using RPG.Saving;

namespace RPG.Cinematics
{
    public class Cinematics : MonoBehaviour, ISaveable
    {
        [SerializeField] bool isTriggered;

       

        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player") && !isTriggered )
            {
                GetComponent<PlayableDirector>().Play();
                isTriggered = true;
            }
        }
        public object CaptureState()
        {
            //Debug.Log(isTriggered);
            return isTriggered;
        }

        public void RestoreState(object state)
        {
           isTriggered = (bool) state;
           //Debug.Log(isTriggered);
        }
    }
}

