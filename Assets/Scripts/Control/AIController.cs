using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using RPG.Core;
using UnityEngine;
using RPG.Movement;
using System;
using UnityEngine.AI;
using RPG.Attributes;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float range = 6f;
        [SerializeField] float suspicionTime = 3f;  
        [SerializeField] float dwellingTime = 3f;
        [SerializeField] PatrolPath patrolPath;  
        Health health;
       
        GameObject player;
        Vector3 guardingPosition;
        int index = 0;
        Vector3? nextWaypointPos = null;
        
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeBeforeNextWaypoint = Mathf.Infinity;
        
        private void Start() {
           player  = GameObject.FindGameObjectWithTag("Player");     
           health = GetComponent<Health>();
           guardingPosition = transform.position;
        }
        // Update is called once per frame
        void Update()
        {
            if (health.IsDead)
            {
                return;
            }

            float distance = Vector3.Distance(transform.position, player.transform.position);
            // GetComponent<Fighter>().CanAttack(player)
            if (distance <= range && GetComponent<Fighter>().CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (distance > range && timeSinceLastSawPlayer <= suspicionTime)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            timeSinceLastSawPlayer += Time.deltaTime;
            
        }

      

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            SetEnemySpeed(5.6f);
            GetComponent<Fighter>().Attack(player);
        }

        private void SetEnemySpeed(float speed)
        {
            GetComponent<NavMeshAgent>().speed = speed;
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void PatrolBehaviour()
        {
            SetEnemySpeed(2f);
            Vector3 nextPos = guardingPosition;
            if(patrolPath != null)
            {
                if(AtWaypoint())
                {
                    CycleWaypoint();
                }
                else if(!AtWaypoint())
                {
                    timeBeforeNextWaypoint = 0;
                }
                nextPos = GetCurrentWaypoint();
            }
            GetComponent<Mover>().StartMoveAction(nextPos);
        }

        private Vector3 GetCurrentWaypoint()
        {
            //Debug.Log(nextWaypointPos);
            
            if(nextWaypointPos == null)
            {
                return patrolPath.GetWaypoint(index);
            }
            
            return (Vector3)nextWaypointPos;
            // patrolPath.GetWaypoint(index);
        }

        private void CycleWaypoint()
        {
            // Another way to do this logic using only indexes
            // Debug.Log("index " + index);
            // Debug.Log("cycle");
            // index += 1;
            // index %= 3;
            
           
           timeBeforeNextWaypoint += Time.deltaTime;
            
            if(timeBeforeNextWaypoint >= dwellingTime)
            {
                
                
                nextWaypointPos = patrolPath.GetNext(index);
                index++;
            }
               
            
            
            //Debug.Log("index " + index);
            
        }

        private bool AtWaypoint()
        {
           float distBetweenWaypoints = Vector3.Distance(transform.position, GetCurrentWaypoint());
           if(distBetweenWaypoints < 1)
           {
               return true;
           } else
           return false;
        }

        private void OnDrawGizmosSelected() {
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(transform.position, range);
            }
    }
}
