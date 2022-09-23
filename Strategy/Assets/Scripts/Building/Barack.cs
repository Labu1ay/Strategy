using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barack : Build {
    public Transform Spawn;

    public void CreateUnit(GameObject unitPrefab) {
        Instantiate(unitPrefab, Spawn.position, Quaternion.identity);
    }

}
