using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine.AI;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float enemySpeed = 3f;
        [SerializeField] private float suspicionTime = 5f;
        
        private Fighter _fighter;
        private GameObject _player;
        private NavMeshAgent _navMeshAgent;
        private Health _health;
        private Mover _mover;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        
        
        // Start is called before the first frame update
        void Start()
        {
            _fighter = GetComponent<Fighter>();
            _player = GameObject.FindWithTag("Player");
            _health = GetComponent<Health>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = enemySpeed;
            _mover = GetComponent<Mover>();

            guardPosition = transform.position;
        }

        // Update is called once per frame
        void Update()
        {
            if (_health.IsDead()) return;
            
            if (InAttackRangeOfPlayer()  && _fighter.CanAttack(_player))
            {
                timeSinceLastSawPlayer = 0;
                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer <= suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                GuardBehavior();
            }

            timeSinceLastSawPlayer += Time.deltaTime;
        }

        private void GuardBehavior()
        {
            _mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            _fighter.Attack(_player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float playerDistance = Vector3.Distance(_player.transform.position, transform.position);
            return playerDistance <= chaseDistance;
        }

        //called by Unity
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
            
        }
    }
}
