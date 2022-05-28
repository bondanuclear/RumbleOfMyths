using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;
namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        
       
        private void Start() {
            player = GameObject.FindWithTag("Player");
           GetComponent<PlayableDirector>().played += DisableControl;
           GetComponent<PlayableDirector>().stopped += EnableControl;
        }
        void DisableControl(PlayableDirector playableDirector)
        {
            // GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
        void EnableControl(PlayableDirector playableDirector)
        {
            
            player.GetComponent<PlayerController>().enabled = true;
           
            
            Debug.Log("Enable control");
        }

       

       
    }
}
