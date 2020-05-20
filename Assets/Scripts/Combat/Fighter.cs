using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using RPG.Movement;
using UnityEngine.Serialization;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        
        [FormerlySerializedAs("fighterRange")] [SerializeField] float weaponRange = 2f;
        [SerializeField] private float timeBetweenAttacks = 0.5f;
        private Transform target;
        private ActionScheduler _scheduler;
        private Mover _mover;
        private Animator _animator;
        private float timeSinceLastAttack;

        private void Start()
        {
            _scheduler = GetComponent<ActionScheduler>();
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            
            if (target == null) return;
            
            if (IsInRange())
            {
                _mover.Cancel();
                AttackBehavior();
            }
            else
            {
                _mover.MoveTo(target.position);
            }
        }

        private void AttackBehavior()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                _animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }

        }

        private bool IsInRange()
        {
            return Vector3.Distance(target.position, transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            _scheduler.StartAction(this);
            
            print("I'm attacking: " + combatTarget.name);
        }

        public void Cancel()
        {
            target = null;
        }
        
        // Animation event
        private void Hit()
        {
            
        }
    }
}

