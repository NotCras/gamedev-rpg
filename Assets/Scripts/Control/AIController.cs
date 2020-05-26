using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using UnityEngine.AI;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float enemySpeed = 3f;

        private Fighter _fighter;
        private GameObject _player;
        private NavMeshAgent _navMeshAgent;
        private Health _health;


        // Start is called before the first frame update
        void Start()
        {
            _fighter = GetComponent<Fighter>();
            _player = GameObject.FindWithTag("Player");
            _health = GetComponent<Health>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = enemySpeed;
        }

        // Update is called once per frame
        void Update()
        {
            if (_health.IsDead()) return;
            
            if (InAttackRangeOfPlayer()  && _fighter.CanAttack(_player))
            {
                print(gameObject.name + " will give chase.");
                _fighter.Attack(_player);
            }
            else
            {
                _fighter.Cancel();
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            float playerDistance = Vector3.Distance(_player.transform.position, transform.position);
            return playerDistance <= chaseDistance;
        }
    }
}
