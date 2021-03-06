﻿using System;
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
        private Health _health;

        [SerializeField] private float maxSpeed = 6f;

        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            _scheduler = GetComponent<ActionScheduler>();
            _health = GetComponent<Health>();
        }

        void Update()
        {
            _navMeshAgent.enabled = !_health.IsDead();
            
            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 point, float speedFraction)
        {
            _scheduler.StartAction(this);
            MoveTo(point, speedFraction);
        }

        public void MoveTo(Vector3 point, float speedFraction)
        {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.destination = point;
            _navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
        }

        public void Cancel()
        {
            _navMeshAgent.isStopped = true;
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
