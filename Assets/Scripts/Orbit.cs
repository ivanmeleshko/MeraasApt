using UnityEngine;
using UnityEngine.UI;


public class Orbit : MonoBehaviour
{

    [SerializeField]
    float SmoothFactor = 0.5f;
    [SerializeField]
    Transform HouseTransform;
    Camera cam;  
    Vector3 firstpoint = new Vector3(0f, 1f, -10f);
    Vector3 secondpoint = new Vector3(0f, 1f, -10f);
    Vector3 center, newPos;
    Image imageCompas;
    protected float RotationsSpeed = 2.0f;
    float speed = 1f;
    float boundsCenterY;
    float sensitivity = 50f;
    float distance;
    bool rotateAround = false;


    void Start()
    {
        cam = Camera.main;
        center = HouseTransform.GetComponent<Renderer>().bounds.center;
        cam.transform.LookAt(center);

        if (HouseTransform != null)
        {
            Settings.instance.cameraOffset = cam.transform.position - center;
            boundsCenterY = center.y + 10;
            distance = Vector3.Distance(cam.transform.position, center);
        }

        imageCompas = GameObject.Find("ImageCompas").GetComponent<Image>();
    }


    void Update()
    {
        if (!Settings.IsPointerOverUIObject() && !Settings.instance.readyToOrbitRotate  && Settings.instance.interactive)
        {
           if (!Settings.instance.touchSupported)
            {
                if (Input.GetMouseButtonDown(0)) rotateAround = true;
                if (Input.GetMouseButtonUp(0)) rotateAround = false;

                if (rotateAround)
                {
                    Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);
                    float posY = cam.transform.position.y - Input.GetAxis("Mouse Y") * RotationsSpeed * 15;
                    Settings.instance.cameraOffset = camTurnAngle * Settings.instance.cameraOffset;

                    newPos = center + Settings.instance.cameraOffset;
                    newPos.y = Mathf.Clamp(posY, boundsCenterY - 5f, boundsCenterY + 100f);

                    cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, SmoothFactor);
                    cam.transform.LookAt(center);

                    imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y - 105);
                }
                //Mouse zoom
                if (Input.GetAxis("Mouse ScrollWheel") != 0 && !Settings.instance.readyToOrbitRotate)
                {
                    float scrollDistance = Input.GetAxis("Mouse ScrollWheel") * sensitivity;

                    if (scrollDistance > 0)
                    {
                        if (Vector3.Distance(cam.transform.position, center) - scrollDistance > 45)
                        {
                            cam.transform.position = Vector3.MoveTowards(cam.transform.position, center, scrollDistance);
                            newPos = cam.transform.position;
                            Settings.instance.cameraOffset = cam.transform.position - center;
                        }
                    }
                    else
                    {
                        if (Vector3.Distance(cam.transform.position, center) - scrollDistance < 120)
                        {
                            cam.transform.position = Vector3.MoveTowards(cam.transform.position, center, scrollDistance);
                            newPos = cam.transform.position;
                            Settings.instance.cameraOffset = cam.transform.position - center;
                        }
                    }
                }
            }
            else
            {
                //Touch camera controller
                if (Input.touchCount == 1 && !Settings.instance.readyToOrbitRotate)
                {
                    if (Input.GetTouch(0).phase == TouchPhase.Began)
                    {
                        firstpoint = Input.GetTouch(0).position;
                    }
                    if (Input.GetTouch(0).phase == TouchPhase.Moved)
                    {
                        secondpoint = Input.GetTouch(0).position;
                        Quaternion camTurnAngle = Quaternion.AngleAxis((secondpoint.x - firstpoint.x) * RotationsSpeed * 0.003f, Vector3.up);

                        float posY = cam.transform.position.y - (secondpoint.y - firstpoint.y) * RotationsSpeed * 0.01f;
                        Settings.instance.cameraOffset = camTurnAngle * Settings.instance.cameraOffset;

                        Vector3 newPos = center + Settings.instance.cameraOffset;
                        newPos.y = Mathf.Clamp(posY, boundsCenterY - 5f, boundsCenterY + 120f);

                        cam.transform.position = newPos;
                        cam.transform.LookAt(center);

                        imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y - 105);
                    }
                }

                if (Input.touchCount == 2 && !Settings.instance.readyToOrbitRotate)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    Vector2 touchZeroPrePos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrePos = touchOne.position - touchOne.deltaPosition;

                    float prevMagnitude = (touchZeroPrePos - touchOnePrePos).magnitude;
                    float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
                    float difference = (currentMagnitude - prevMagnitude) * SmoothFactor;

                    if (Vector3.Distance(cam.transform.position, center) - difference > 45
                      && Vector3.Distance(cam.transform.position, center) - difference < 120)
                    {
                        cam.transform.position = Vector3.MoveTowards(cam.transform.position, center, difference);
                        newPos = cam.transform.position;
                        Settings.instance.cameraOffset = cam.transform.position - center;
                    }
                }
            }
        }
    }

}
