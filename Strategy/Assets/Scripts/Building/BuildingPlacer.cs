using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour {
    public static BuildingPlacer S;

    public float CellSize = 1f;
    public Camera Camera;
    private Plane _plane;

    public Build CurrentBuild;
    void Start() {
        S = this;
        _plane = new Plane(Vector3.up, Vector3.zero);
    }


    void Update() {

        if(CurrentBuild == null) {
            return;
        }
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        float distance;
        _plane.Raycast(ray, out distance);
        Vector3 point = ray.GetPoint(distance)/CellSize;

        int x = Mathf.RoundToInt(point.x);
        int z = Mathf.RoundToInt(point.z);

        CurrentBuild.transform.position = new Vector3(x, 0f, z) * CellSize;

        if (Input.GetMouseButtonDown(0)) {
            CurrentBuild = null;
        }
    }
    public void CreateBuilding(GameObject buildingPrefab) {
        GameObject newBuilding = Instantiate(buildingPrefab);
        CurrentBuild = newBuilding.GetComponent<Build>();
    }
}
