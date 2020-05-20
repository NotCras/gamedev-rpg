using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using UnityEngine.Serialization;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour
    {
        
        [FormerlySerializedAs("fighterRange")] [SerializeField] float weaponRange = 2f;
        private Transform target;
        private Mover _mover;

        private void Start()
        {
            _mover = GetComponent<Mover>();
        }

        private void Update()
        {
            if (target == null) return;
            
            if (IsInRange())
            {
                _mover.Stop();
            }
            else
            {
                _mover.MoveTo(target.position);
            }
        }

        private bool IsInRange()
        {
            return Vector3.Distance(target.position, transform.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            target = combatTarget.transform;
            print("I'm attacking: " + combatTarget.name);
        }

        public void Cancel()
        {
            target = null;
        }
    }
}

