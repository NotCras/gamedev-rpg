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
        [SerializeField] private float timeBetweenAttacks = 0.5f; //for now, can't be greater than 0.5
        [SerializeField] private float fistDamage = 10f;
        
        private Transform target;
        private ActionScheduler _scheduler;
        private Mover _mover;
        private Animator _animator;
        private float timeSinceLastAttack = Mathf.Infinity;

        private void Start()
        {
            _scheduler = GetComponent<ActionScheduler>();
            _mover = GetComponent<Mover>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;
            print(timeSinceLastAttack);
            
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
                // this will trigger hit event
                _animator.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }

        }
        
        // Animation event
        private void Hit()
        {
            if (target == null) return;

            target.GetComponent<Health>().TakeDamage(fistDamage);
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
        

    }
}

