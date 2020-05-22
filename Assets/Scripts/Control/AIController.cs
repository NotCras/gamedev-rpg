using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        
        private 
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            GameObject player = GameObject.FindWithTag("Player");

            float playerDistance = Vector3.Distance(player.transform.position, transform.position);

            if (playerDistance <= chaseDistance)
            {
                print(gameObject.name + " will give chase.");
            }
        }
    }
}
