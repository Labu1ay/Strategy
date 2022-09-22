using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Management : MonoBehaviour
{
    public Camera Camera;
    public SelectbleObject Howered;

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
            else
            {
                UnhoverCurrent();
            }
        }
        else
        {
            UnhoverCurrent();
        }
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
