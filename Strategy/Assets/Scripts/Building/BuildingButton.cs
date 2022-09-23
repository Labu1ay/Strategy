using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingButton : MonoBehaviour
{
    public GameObject BuildingPrefab;

    public void TryBuy() {
        int price = BuildingPrefab.GetComponent<Build>().Price;
        if (Resources.S.Money >= price) {
            Resources.S.Money -= price;
            BuildingPlacer.S.CreateBuilding(BuildingPrefab);
        }
    }
}
