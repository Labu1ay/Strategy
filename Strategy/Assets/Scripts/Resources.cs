using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public static Resources S;
    public int Money;

    private void Start() {
        S = this;
    }
}
