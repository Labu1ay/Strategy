using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacer : MonoBehaviour {
    public static BuildingPlacer S;

    public float CellSize = 1f;
    public Camera Camera;
    private Plane _plane;

    public Build CurrentBuild;

    public Dictionary<Vector2Int, Build> BuildingsDictionary = new Dictionary<Vector2Int, Build>();
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

        if (CheckAllow(x, z, CurrentBuild)) {
            CurrentBuild.DisplayAcceptablePosition();

            if (Input.GetMouseButtonDown(0)) {
                InstallBuilding(x, z, CurrentBuild);
                CurrentBuild = null;
            }
        } else {
            CurrentBuild.DisplayUnacceptablePosition();
        }
    }

    bool CheckAllow(int xPosition, int zPosition, Build building) {
        for (int x = 0; x < building.XSize; x++) {
            for (int z = 0; z < building.ZSize; z++) {
                Vector2Int coordinate = new Vector2Int(xPosition + x, zPosition + z);
                if (BuildingsDictionary.ContainsKey(coordinate)) {
                    return false;
                }
            }
        }
        return true;
    }

    void InstallBuilding(int xPosition, int zPosition, Build building) {
        for (int x = 0; x < building.XSize; x++) {
            for (int z = 0; z < building.ZSize; z++) {
                Vector2Int coordinate = new Vector2Int(xPosition + x, zPosition + z);
                BuildingsDictionary.Add(coordinate, building);
            }

        }
        foreach (var item in BuildingsDictionary) {
            Debug.Log(item);
        }
    }
    public void CreateBuilding(GameObject buildingPrefab) {
        GameObject newBuilding = Instantiate(buildingPrefab);
        CurrentBuild = newBuilding.GetComponent<Build>();
    }
}
