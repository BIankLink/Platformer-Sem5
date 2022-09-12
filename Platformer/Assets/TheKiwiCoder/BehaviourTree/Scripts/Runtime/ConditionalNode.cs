using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

namespace TheKiwiCoder 
{
    public abstract class ConditionalNode : Node
    {
        public abstract bool Condition();

        protected override void OnStart()
        {
            
        }

        protected override void OnStop()
        {
            
        }

        protected override State OnUpdate()
        {
            bool bResult = Condition();
            return bResult ? State.Success: State.Failure;
        }
    } 
}
