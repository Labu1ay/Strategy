using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barack : Build {
    public Transform Spawn;

    public void CreateUnit(GameObject unitPrefab) {
        GameObject newUnit = Instantiate(unitPrefab, Spawn.position, Quaternion.identity);
        Vector3 position = Spawn.position + new Vector3(Random.Range(-1.5f, 1.5f), 0f, Random.Range(-1.5f, 1.5f));
        newUnit.GetComponent<Unit>().WhenClickOnGround(position);
    }

}
