using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] private float waypointGizmoRadius = 0.4f;

        private void OnDrawGizmos()
        {
            Transform tPrev = transform.GetChild(transform.childCount - 1);
            
            for (int i = 0; i < transform.childCount; i++)
            {
                Transform t = transform.GetChild(i);
                
                Gizmos.DrawSphere(t.position, waypointGizmoRadius);
                Gizmos.DrawLine(t.position, tPrev.position);

                tPrev = t;
            }
        }

        public float GetPathSize()
        {
            return transform.childCount;
        }

        public Vector3 GetWaypointChildPosition(int index)
        {
            return transform.GetChild(index).position;
        }
    }
}
