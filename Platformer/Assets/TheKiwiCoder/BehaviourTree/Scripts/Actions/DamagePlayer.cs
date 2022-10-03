using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using Unity.Burst.CompilerServices;

public class DamagePlayer : ActionNode
{
    protected override void OnStart() {
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {

        IDamageable damageableObject = context.fov.visibleTarget.gameObject.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            damageableObject.TakeDamage(blackboard.damage);
            return State.Running;
        }
        if (damageableObject == null)
        {
            return State.Failure;
        }
        
        return State.Success;
    }
}
