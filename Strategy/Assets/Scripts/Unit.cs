using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectbleObject {
    public int Health;
    public int Price;
    public NavMeshAgent NavMeshAgent;
    public override void OnClickOnGround(Vector3 point) {
        base.OnClickOnGround(point);

        NavMeshAgent.SetDestination(point);
    }
    public void TakeDamage(int damageValue) {
        Health -= damageValue;
        if(Health <= 0) {
            Destroy(gameObject);
        }
    }
}
