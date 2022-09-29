using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum SelectionState {
    UnitsSelected,
    Frame,
    Other
}

public class Management : MonoBehaviour {
    public Camera Camera;
    public SelectbleObject Howered;
    public List<SelectbleObject> ListOfSelected = new List<SelectbleObject>();

    public Image FrameImage;
    private Vector2 _frameStart;
    private Vector2 _frameEnd;

    public SelectionState CurrentSelectionState;

    private void Start() {
        FrameImage.enabled = false;
    }
    void Update() {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            if (hit.collider.GetComponent<SelectableCollider>()) {
                SelectbleObject hitSelectable = hit.collider.GetComponent<SelectableCollider>().SelectbleObject;
                if (Howered) {
                    if (Howered != hitSelectable) {
                        Howered.OnUnhover();
                        Howered = hitSelectable;
                        Howered.OnHover();
                    }
                } else {
                    Howered = hitSelectable;
                    Howered.OnHover();
                }
            } else UnhoverCurrent();

        } else UnhoverCurrent();

        if (Input.GetMouseButtonUp(0)) {
            if (Howered) {
                if (Input.GetKey(KeyCode.LeftControl) == false) {
                    UnselectAll();
                }
                CurrentSelectionState = SelectionState.UnitsSelected;
                Select(Howered);
            }
        }
        if (CurrentSelectionState == SelectionState.UnitsSelected) {
            if (Input.GetMouseButtonUp(0)) {
                if (hit.collider.tag == "Ground") {

                    int rowNumber = Mathf.CeilToInt(Mathf.Sqrt(ListOfSelected.Count));


                    for (int i = 0; i < ListOfSelected.Count; i++) {

                        int row = i / rowNumber;
                        int column = i % rowNumber;

                        Vector3 point = hit.point + new Vector3(row, 0f, column);

                        ListOfSelected[i].WhenClickOnGround(point);
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            UnselectAll();
        }

        if (Input.GetMouseButtonDown(0)) {
            _frameStart = Input.mousePosition;
        }

        if (Input.GetMouseButton(0)) {
            _frameEnd = Input.mousePosition;

            Vector2 min = Vector2.Min(_frameStart, _frameEnd);
            Vector2 max = Vector2.Max(_frameStart, _frameEnd);
            Vector2 size = max - min;

            if (size.magnitude > 10) {
                FrameImage.enabled = true;

                FrameImage.rectTransform.anchoredPosition = min;
                FrameImage.rectTransform.sizeDelta = size;

                UnselectAll();
                Rect rect = new Rect(min, size);
                Unit[] allUnits = FindObjectsOfType<Unit>();
                for (int i = 0; i < allUnits.Length; i++) {
                    Vector2 screenPosition = Camera.WorldToScreenPoint(allUnits[i].transform.position);
                    if (rect.Contains(screenPosition)) {
                        Select(allUnits[i]);
                    }
                }
                CurrentSelectionState = SelectionState.Frame;
            }
        }

        if (Input.GetMouseButtonUp(0)) {
            FrameImage.enabled = false;
            if (ListOfSelected.Count > 0) {
                CurrentSelectionState = SelectionState.UnitsSelected;
            } else {
                CurrentSelectionState = SelectionState.Other;
            }
        }

    }

    void Select(SelectbleObject selectbleObject) {

        if (ListOfSelected.Contains(selectbleObject) == false) {
            ListOfSelected.Add(selectbleObject);
            selectbleObject.Select();
        }
    }

    public void Unselect(SelectbleObject selectbleObject) {
        if (ListOfSelected.Contains(selectbleObject)) {
            ListOfSelected.Remove(selectbleObject);
        }
    }


    void UnselectAll() {
        for (int i = 0; i < ListOfSelected.Count; i++) {
            ListOfSelected[i].Unselect();
        }
        ListOfSelected.Clear();
        CurrentSelectionState = SelectionState.Other;
    }


    void UnhoverCurrent() {
        if (Howered) {
            Howered.OnUnhover();
            Howered = null;
        }
    }
}
