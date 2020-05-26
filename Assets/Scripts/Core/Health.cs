using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG.Core
{
    public class Health : MonoBehaviour
    {
        [FormerlySerializedAs("health")] [SerializeField] private float healthPoints = 100f;
        
        private bool isDead = false;
        private ActionScheduler _actionScheduler;

        private void Start()
        {
            _actionScheduler = GetComponent<ActionScheduler>();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            print("Health: " + healthPoints);

            if (healthPoints == 0 && !isDead)
            {
                Die();
            }
        }

        public bool IsDead()
        {
            return isDead;
        }

        private void Die()
        {
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            _actionScheduler.CancelCurrentAction();
        }
    }
    
}
