using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
        private Fighter _fighter;
        private Health _health;

        void Start()
        {
            _mover = GetComponent<Mover>();
            _fighter = GetComponent<Fighter>();
            _health = GetComponent<Health>();
        }
    
        void Update()
        {
            if (_health.IsDead()) return;
            if(InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            
        }

        private bool InteractWithCombat()
        {
            Ray ray = GetMouseRay();
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (var hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();

                if (target == null) continue;
                
                if (_fighter.CanAttack(target.gameObject))
                {
                    continue;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    _fighter.Attack(target.gameObject);
                }

                return true;
            }

            return false;
        }

        private bool InteractWithMovement()
        {
            var ray = GetMouseRay();
            RaycastHit hitInfo;
            bool hasHit = Physics.Raycast(ray, out hitInfo);

            if (hasHit)
            {
                if (Input.GetMouseButton(1)) _mover.StartMoveAction(hitInfo.point);
                return true;
            }

            return false;
        }

        private static Ray GetMouseRay()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return ray;
        }
    }

}
