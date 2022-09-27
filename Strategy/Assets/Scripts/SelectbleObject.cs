using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectbleObject : MonoBehaviour {
    public GameObject SelectbleIndicator;

    public virtual void Start() {
        SelectbleIndicator.SetActive(false);
    }
    public virtual void OnHover() {
        transform.localScale = Vector3.one * 1.1f;
    }
    public virtual void OnUnhover() {
        transform.localScale = Vector3.one;
    }

    public virtual void Select() {
        SelectbleIndicator.SetActive(true);
    }
    public virtual void Unselect() {
        SelectbleIndicator.SetActive(false);
    }
    public virtual void WhenClickOnGround(Vector3 point) {
    }
}
