using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using RPG.Combat;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {
        private Mover _mover;
    
        void Start()
        {
            _mover = GetComponent<Mover>();

        }
    
        void Update()
        {
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

                if (target == null)
                {
                    continue;
                }

                if (Input.GetMouseButtonDown(1))
                {
                    GetComponent<Fighter>().Attack(target);

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
