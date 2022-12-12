using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheKiwiCoder
{
    public class PlayerCheck : ConditionalNode
    {
        public Transform target;
        Transform player;
        public override bool Condition()
        {
            
            if(target == null)
            {
                return false;
            }
            return true;
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
                player = target;
                
                
            }
            if(player!= null)
            {
                player.gameObject.GetComponent<PlayerStateMachine>().MoveSpeed *= 2;
                player = null;
            }
            
            return base.OnUpdate();
        }

    }
}
