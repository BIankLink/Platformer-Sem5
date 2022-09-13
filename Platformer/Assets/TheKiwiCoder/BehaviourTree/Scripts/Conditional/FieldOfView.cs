using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    public class FieldOfView : ConditionalNode
    {
        public float viewRadius;
        [Range(0, 360)]
        public float viewAngle;

        public LayerMask targetMask;
        public LayerMask obstacleMask;

        public Transform visibleTarget;
        public GameObject itself;
         
        
        public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                angleInDegrees += itself.transform.eulerAngles.y;
            }

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }

        public override bool Condition()
        {
            visibleTarget = null;
            Collider[] targetsInViewRadius = Physics.OverlapSphere(itself.transform.position, viewRadius, targetMask);

            foreach (Collider selectedTargets in targetsInViewRadius)
            {
                Transform target = selectedTargets.transform;
                Vector3 dirToTarget = (target.position - itself.transform.position).normalized;
                if (Vector3.Angle(itself.transform.forward, dirToTarget) < viewAngle / 2)
                {
                    float disToTarget = Vector3.Distance(itself.transform.position, target.position);

                    if (!Physics.Raycast(itself.transform.position, dirToTarget, disToTarget, obstacleMask))
                    {
                        visibleTarget = target;
                        blackboard.Player = target;
                    }
                }
            }
            return visibleTarget;
        }

        protected override void OnStart()
        {
            itself =context.gameObject;
            viewRadius = context.sensor.distance;
            viewAngle = context.sensor.angle;
            base.OnStart();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override State OnUpdate()
        {
            return base.OnUpdate();
        }
    }
}
