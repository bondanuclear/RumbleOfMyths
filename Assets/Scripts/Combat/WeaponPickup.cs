using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        private void OnTriggerEnter(Collider other) {
            if(other.CompareTag("Player"))
            {
                other.GetComponent<Fighter>().EquipWeapon(weapon);
                // Destroy(gameObject);
                StartCoroutine(RespawnPickup());
            }
        }

        IEnumerator RespawnPickup()
        {
            ProcessRespawn(false);
            yield return new WaitForSeconds(5f);
            ProcessRespawn(true);
            
        }

        private void ProcessRespawn(bool boolState)
        {
            transform.GetComponent<Collider>().enabled = boolState;
            foreach(Transform child in transform)
            {
                if(child.GetComponent<MeshRenderer>() != null)
                {
                    child.GetComponent<MeshRenderer>().enabled = boolState;
                }
            }
        }
        

       
    }

}