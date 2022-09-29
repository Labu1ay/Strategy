using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : SelectbleObject {
    public int Health;
    private int _maxHealth;
    public int Price;
    public NavMeshAgent NavMeshAgent;

    public GameObject HealthBarPrefab;
    public GameObject PointPrefab;
    private HealthBar _healthBar;

    public override void Start() {
        base.Start();
        _maxHealth = Health;
        GameObject healthBar = Instantiate(HealthBarPrefab);
        _healthBar = healthBar.GetComponent<HealthBar>();
        _healthBar.Setup(transform);
    }
    public override void WhenClickOnGround(Vector3 point) {
        base.WhenClickOnGround(point);
        GameObject newPointPrefab = Instantiate(PointPrefab, point, Quaternion.identity);
        Destroy(newPointPrefab, 0.5f);
        NavMeshAgent.SetDestination(point);
    }
    public void TakeDamage(int damageValue) {
        Health -= damageValue;
        _healthBar.SetHealth(Health, _maxHealth);
        if(Health <= 0) {
            if (_healthBar) {
                Destroy(_healthBar.gameObject);
            }
            FindObjectOfType<Management>().Unselect(this);
            Destroy(gameObject);
        }
    }
}
