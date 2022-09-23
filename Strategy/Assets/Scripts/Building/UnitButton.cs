using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitButton : MonoBehaviour {
    public GameObject UnitPrefab;
    public Text PriceText;
    public Barack Barack;

    private void Start() {
        PriceText.text = UnitPrefab.GetComponent<Unit>().Price.ToString();
    }
    public void TryBuy() {
        int price = UnitPrefab.GetComponent<Unit>().Price;
        if (Resources.S.Money >= price) {
            Resources.S.Money -= price;
            Barack.CreateUnit(UnitPrefab);
        } else {
            Debug.Log("No Money");
        }
    }
}
