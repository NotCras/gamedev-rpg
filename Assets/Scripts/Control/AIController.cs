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
        [SerializeField] private PatrolPath _patrolPath = null;
        [SerializeField] private float waypointTolerance = 0.5f;
        [SerializeField] private float waypointDwellTime = 3f;
        
        private Fighter _fighter;
        private GameObject _player;
        private NavMeshAgent _navMeshAgent;
        private Health _health;
        private Mover _mover;

        private Vector3 guardPosition;
        private float timeSinceLastSawPlayer = Mathf.Infinity;
        private float timeSinceArrivedAtWaypoint = Mathf.Infinity;
        private int pathIndex = 0;
        
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

                AttackBehavior();
            }
            else if (timeSinceLastSawPlayer <= suspicionTime)
            {
                SuspicionBehavior();
            }
            else
            {
                PatrolBehavior();
            }

            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceArrivedAtWaypoint += Time.deltaTime;
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = guardPosition;

            if (_patrolPath != null)
            {
                if (AtWaypoint())
                {
                    timeSinceArrivedAtWaypoint = 0;
                    CycleWaypoint();
                }

                nextPosition = GetCurrentWaypoint();
            }

            if (timeSinceArrivedAtWaypoint > waypointDwellTime)
            {
                _mover.StartMoveAction(nextPosition);
            }
            
        }

        private Vector3 GetCurrentWaypoint()
        {
            return _patrolPath.GetWaypointChildPosition(pathIndex);
        }

        private void CycleWaypoint()
        {
            pathIndex++;
            if (pathIndex >= _patrolPath.GetPathSize())
            {
                pathIndex = 0;
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void SuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            timeSinceLastSawPlayer = 0;
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
