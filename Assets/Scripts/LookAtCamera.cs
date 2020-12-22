using UnityEngine;


public class LookAtCamera : MonoBehaviour
{

    Camera cam;


    private void Start()
    {
        cam = Camera.main;
    }


    void Update()
    {
        transform.eulerAngles = new Vector3(0, cam.transform.eulerAngles.y - 180, 0);
    }

}
