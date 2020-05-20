using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Core;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private ActionScheduler _scheduler;

        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _scheduler = GetComponent<ActionScheduler>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 point)
        {
            _scheduler.StartAction(this);
            MoveTo(point);
        }

        public void MoveTo(Vector3 point)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = point;

        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
            //print("stopped navmeshagent.");
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = _navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            float speed = Math.Abs(localVelocity.z); //localVelocity.z;
            
            _animator.SetFloat("forwardSpeed", speed);
        }
    }

}
