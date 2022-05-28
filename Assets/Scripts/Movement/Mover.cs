using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;
using RPG.Core;
namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        NavMeshAgent navMeshAgent;
        //[SerializeField] Transform target;
        Animator animator;

        // Start is called before the first frame update
        void Start()
        {
            animator = GetComponent<Animator>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        // Update is called once per frame
        void Update()
        {
            // Vector3 dir = (target.position - transform.position).normalized * Time.deltaTime;
            //navMeshAgent.Move(dir);
            // if (Input.GetKey(KeyCode.Mouse0))
            // {
            //     MoveToCursor();

            // }
            ProcessAnimation();

        }

        private void ProcessAnimation()
        {
            Vector3 localVelocity = transform.InverseTransformDirection(navMeshAgent.velocity);
            animator.SetFloat("Blend", localVelocity.z);
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
            //GetComponent<Fighter>().Cancel();

        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.isStopped = false;
            navMeshAgent.destination = destination;
        }
        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        public object CaptureState()
        {
            return new SerializableVector3(transform.position);
        }

        public void RestoreState(object state)
        {
            SerializableVector3 pos = (SerializableVector3) state;
            GetComponent<NavMeshAgent>().enabled = false;
            transform.position = pos.ToVector();
            GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}
