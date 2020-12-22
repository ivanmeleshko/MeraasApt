using UnityEngine;
   

public class ZoomCamera : MonoBehaviour
{

    Camera cam;
    float minFov = 10f;
    float maxFov = 45f;
    float sensitivity = 50f;
    float damping = 5f;
    float distance = 60f;
    float zoomSpeed = 0.1f;
    float previousDistance = 0;
    Vector3 zoomCenter = Vector3.zero;


    private void Start()
    {
        cam = Camera.main;
        distance = cam.fieldOfView;
    }


    private void LateUpdate()
    {
        if (!Settings.IsPointerOverUIObject())
        {
            if (!Settings.instance.orbitRotation && Settings.instance.interactive)
            {
                if (Settings.instance.readyToOrbitRotate)
                {
                    if (Input.GetAxis("Mouse ScrollWheel") != 0)
                    {
                        distance -= Input.GetAxis("Mouse ScrollWheel") * sensitivity;
                        distance = Mathf.Clamp(distance, minFov, maxFov);
                        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, distance, Time.deltaTime * damping);
                    }

                    /*if (Input.touchCount == 2)
                    {
                        Touch touchZero = Input.GetTouch(0);
                        Touch touchOne = Input.GetTouch(1);

                        Vector2 touchZeroPreviousPosition = touchZero.position - touchZero.deltaPosition;
                        Vector2 touchOnePreviousPosition = touchOne.position - touchOne.deltaPosition;

                        float prevTouchDeltaMag = (touchZeroPreviousPosition - touchOnePreviousPosition).magnitude;
                        float TouchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                        float deltaMagDiff = prevTouchDeltaMag - TouchDeltaMag;

                        cam.fieldOfView += deltaMagDiff * zoomSpeed;
                        cam.fieldOfView = Mathf.Clamp(cam.fieldOfView, 10f, 45f);
                    }*/
                }
                else
                {
                    /*float ScrollWheelChange = Input.GetAxis("Mouse ScrollWheel");

                    if (ScrollWheelChange != 0)                                                 //If the scrollwheel has changed
                    {
                        float R = ScrollWheelChange * 30;                                       //The radius from current camera
                        float PosX = Camera.main.transform.eulerAngles.x + 90;                  //Get up and down
                        float PosY = -1 * (Camera.main.transform.eulerAngles.y - 90);           //Get left to right
                        PosX = PosX / 180 * Mathf.PI;                                           //Convert from degrees to radians
                        PosY = PosY / 180 * Mathf.PI;
                        float X = R * Mathf.Sin(PosX) * Mathf.Cos(PosY);                        //Calculate new coords
                        float Z = R * Mathf.Sin(PosX) * Mathf.Sin(PosY);
                        float Y = R * Mathf.Cos(PosX);
                        float CamX = Camera.main.transform.position.x;                          //Get current camera postition for the offset
                        float CamY = Mathf.Clamp(Camera.main.transform.position.y, -70, 0);
                        float CamZ = Camera.main.transform.position.z;
                        Camera.main.transform.position = new Vector3(Mathf.Clamp(CamX + X, -270, 100),
                                                                     Mathf.Clamp(CamY + Y, -70, 0),
                                                                     Mathf.Clamp(CamZ + Z, -500, -130)); //Move the main camera
                    }*/
                }
            }
        }
    }

}