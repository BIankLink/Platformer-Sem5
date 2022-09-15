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
            return base.OnUpdate();
        }
    }
}
