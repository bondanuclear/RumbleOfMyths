using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

using RPG.Attributes;

namespace RPG.Control
{
public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Health health;
        // Start is called before the first frame update
        void Start()
        {
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
        }

        // Update is called once per frame
        void Update()
        {
            if(health.IsDead)
            {
                return;
            }
            if(InteractWithCombat()) return;
            if(InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] raycastHits = Physics.RaycastAll(GetRay());
            foreach(RaycastHit hit in raycastHits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) continue;    
                if(!GetComponent<Fighter>().CanAttack(target.gameObject)) continue;
                if(target != null)
                {
                    //
                    if(Input.GetKey(KeyCode.Mouse0))
                    {
                    //    Debug.Log(GetComponent<Fighter>().CanAttack());
                        GetComponent<Fighter>().Attack(target.gameObject);
                    }
                   
                }
                // else continue;
                return true;
            }
            return false;
        }
        private bool InteractWithMovement()
        {
            Ray ray = GetRay();
            RaycastHit raycastHit;
            if (Physics.Raycast(ray, out raycastHit))
            {
               
                if (Input.GetKey(KeyCode.Mouse0))
                { 
                    mover.StartMoveAction(raycastHit.point);
                    //GetComponent<Fighter>().StopAttack();
                }
                return true;

            } 
            return false;       
            // lastRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        }

        private static Ray GetRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}

