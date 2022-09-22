using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectbleObject {
    public NavMeshAgent NavMeshAgent;
    public override void OnClickOnGround(Vector3 point) {
        base.OnClickOnGround(point);

        NavMeshAgent.SetDestination(point);
    }
}
