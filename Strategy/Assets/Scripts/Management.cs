using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Management : MonoBehaviour
{
    public Camera Camera;
    public SelectbleObject Howered;
    public List<SelectbleObject> ListOfSelected = new List<SelectbleObject>();

    void Update()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponent<SelectableCollider>())
            {
                SelectbleObject hitSelectable = hit.collider.GetComponent<SelectableCollider>().SelectbleObject;
                if (Howered)
                {
                    if(Howered != hitSelectable)
                    {
                        Howered.OnUnhover();
                        Howered = hitSelectable;
                        Howered.OnHover();
                    }
                }
                else
                {
                    Howered = hitSelectable;
                    Howered.OnHover();
                }
            }
            else UnhoverCurrent();
            
        }
        else UnhoverCurrent();

        if (Input.GetMouseButtonUp(0))
        {
            if (Howered)
            {
                if (Input.GetKey(KeyCode.LeftControl) == false) 
                {
                    UnselectAll();
                }
                Select(Howered);
            }
            if (hit.collider.tag == "Ground")
            {
                for (int i = 0; i < ListOfSelected.Count; i++)
                {
                    ListOfSelected[i].OnClickOnGround(hit.point);
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            UnselectAll();
        }
        
    }

    void Select(SelectbleObject selectbleObject)
    {

        if (ListOfSelected.Contains(selectbleObject) == false)
        {
            ListOfSelected.Add(selectbleObject);
            selectbleObject.Select();
        }
    }
    

    
    void UnselectAll()
    {
        for (int i = 0; i < ListOfSelected.Count; i++)
        {
            ListOfSelected[i].Unselect();
        }
        ListOfSelected.Clear();
    }


    void UnhoverCurrent()
    {
        if (Howered)
        {
            Howered.OnUnhover();
            Howered = null;
        }
    }
}
