using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState {
    Idle,
    WalkToBuilding,
    WalkToUnit,
    Attack
}
public class Enemy : MonoBehaviour {
    public EnemyState CurrentEnemyState;

    public int Health;
    public Build TargetBuilding;
    public Unit TargetUnit;
    public float DistanceToFollow = 7f;
    public float DistanceToAttack = 1f;

    public NavMeshAgent NavMeshAgent;

    public float AttackPeriod = 1f;
    private float _timer;

    private void Start() {
        SetState(EnemyState.WalkToBuilding);  
    }
    void Update() {
        if (CurrentEnemyState == EnemyState.Idle) {
            FindClosestUnit();
        } else if (CurrentEnemyState == EnemyState.WalkToBuilding) {
            FindClosestUnit();
            if(TargetBuilding == null) {
                SetState(EnemyState.Idle);
            }
        } else if (CurrentEnemyState == EnemyState.WalkToUnit) {
            if (TargetUnit) {
                NavMeshAgent.SetDestination(TargetUnit.transform.position);
                float distance = Vector3.Distance(transform.position, TargetUnit.transform.position);
                if (distance > DistanceToFollow) {
                    SetState(EnemyState.WalkToBuilding);
                }
                if (distance < DistanceToAttack) {
                    SetState(EnemyState.Attack);
                }
            } else {
                SetState(EnemyState.WalkToBuilding);
            }
        } else if (CurrentEnemyState == EnemyState.Attack) {
            if (TargetUnit) {
                float distance = Vector3.Distance(transform.position, TargetUnit.transform.position);
                if (distance > DistanceToAttack) {
                    SetState(EnemyState.WalkToUnit);
                }
                _timer += Time.deltaTime;
                if (_timer >= AttackPeriod) {
                    _timer = 0f;
                    TargetUnit.TakeDamage(1);
                }
            } else {
                SetState(EnemyState.WalkToBuilding);
            }
        }
    }

    public void SetState(EnemyState enemyState) {
        CurrentEnemyState = enemyState;
        if (CurrentEnemyState == EnemyState.Idle) {

        } else if (CurrentEnemyState == EnemyState.WalkToBuilding) {
            FindClosestBuilding();
            NavMeshAgent.SetDestination(TargetBuilding.transform.position);
        } else if (CurrentEnemyState == EnemyState.WalkToUnit) {

        } else if (CurrentEnemyState == EnemyState.Attack) {
            _timer = 0f;
        }
    }

    public void FindClosestBuilding() {
        Build[] allBuilding = FindObjectsOfType<Build>();
        float minDistance = Mathf.Infinity;
        Build closestBuilding = null;

        for (int i = 0; i < allBuilding.Length; i++) {
            float distance = Vector3.Distance(transform.position, allBuilding[i].transform.position);
            if(distance < minDistance) {
                minDistance = distance;
                closestBuilding = allBuilding[i];
            }
        }
        TargetBuilding = closestBuilding;
    }

    public void FindClosestUnit() {
        Unit[] allUnits = FindObjectsOfType<Unit>();
        float minDistance = Mathf.Infinity;
        Unit closestUnit = null;

        for (int i = 0; i < allUnits.Length; i++) {
            float distance = Vector3.Distance(transform.position, allUnits[i].transform.position);
            if (distance < minDistance) {
                minDistance = distance;
                closestUnit = allUnits[i];
            }
        }
        if (minDistance < DistanceToFollow) {
            TargetUnit = closestUnit;
            CurrentEnemyState = EnemyState.WalkToUnit;
        }
        
    }
}
