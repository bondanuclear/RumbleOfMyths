using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float sphereSize;
        private void OnDrawGizmos() {
            Gizmos.color = Color.white;
            for(int i =0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(GetWaypoint(i), sphereSize);
                // Debug.Log($"{GetWaypoint(0)} {GetWaypoint(1)}");
                
                //Gizmos.DrawRay(GetWaypoint(0), GetWaypoint(1));
                Gizmos.DrawLine(GetWaypoint(i),GetNext(i));
            }
        }
        public Vector3 GetNext(int i)
        {
            return GetWaypoint((i + 1) % transform.childCount);
        }
        public Vector3 GetWaypoint(int i)
        {
            return transform.GetChild(i).position;
        }
    }

}