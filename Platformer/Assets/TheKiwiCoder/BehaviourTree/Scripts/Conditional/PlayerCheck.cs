using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    public class PlayerCheck : ConditionalNode
    {
        public Transform target;
        
        public override bool Condition()
        {
            
            return target;
        }

        protected override void OnStart()
        {
            target = context.fov.visibleTarget;
            
            base.OnStart();
            
        }

        protected override void OnStop()
        {
          
            base.OnStop();
        }

        protected override State OnUpdate()
        {
            if (target != null)
            {
                target.gameObject.GetComponent<PlayerStateMachine>().MoveSpeed /= 2;
                Debug.Log(target.gameObject.GetComponent<PlayerStateMachine>().MoveSpeed);
                
            }
            
            return base.OnUpdate();
        }

        IEnumerator slow()
        {
            target.gameObject.GetComponent<PlayerStateMachine>().MoveSpeed /= 2;
            yield return new WaitForSeconds(5f);
            target.gameObject.GetComponent<PlayerStateMachine>().MoveSpeed *= 2;

        }
    }
}
