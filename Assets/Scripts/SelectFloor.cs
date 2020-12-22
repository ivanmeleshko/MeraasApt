using System.Collections.Generic;
using UnityEngine;


public class SelectFloor : MonoBehaviour
{

    public List<Transform> floors;
    public Transform selectedFloor, highlights;



    public void OnValueChanged(float value)
    {
        HideFloor((int)value);
    }


    private void HideFloor(int floor)
    {
        if (floor == 0)
        {
            foreach (Transform f in floors)
            {
                f.gameObject.SetActive(true);
            }
            selectedFloor.position = new Vector3(0, 1.8f, 0);
            //highlights.gameObject.SetActive(false);
        }
        else
        {
            for (int i = 1; i < floors.Count; i++)
            {
                if (i < floor)
                {
                    floors[i].gameObject.SetActive(true);                
                }
                else
                {
                    floors[i].gameObject.SetActive(false);
                }
            }
            selectedFloor.position = new Vector3(0, 3.7f * (floor - 1) + 1.8f, 0);
            //highlights.gameObject.SetActive(true);
        }
        
    }


    public void OnMouseDown()
    {
        //if (UI.DoubleClick())
        {
            if (gameObject != null)
            {
                Animator animator = gameObject.GetComponent<Animator>();

                if (animator != null)
                {
                    bool show = animator.GetBool("show");
                    animator.SetBool("show", !show);
                }
            }          
        }
    }

}
