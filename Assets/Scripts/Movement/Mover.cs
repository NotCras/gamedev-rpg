using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Combat;


namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private Fighter _fighter;

        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _fighter = GetComponent<Fighter>();
        }

        void Update()
        {
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 point)
        {
            _fighter.Cancel();
            MoveTo(point);
        }

        public void MoveTo(Vector3 point)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = point;

        }

        public void Stop()
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
