using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NavigateCamera : MonoBehaviour
{

    [SerializeField]
    float Speed = 2.0f;
    [SerializeField]
    GameObject Popup;
    [SerializeField]
    float damping = 5f;
    [SerializeField]
    [Range(0.01f, 1.0f)]
    float SmoothFactor = 0.5f;
    [SerializeField]
    bool LookAtPlayer = false;

    Camera cam;   
    GameObject buttonFlatName;
    Material trMat;
    Image imageCompas;
    Vector3 center, newPos;
    Vector3 firstpoint = new Vector3(0f, 1f, -10f);
    Vector3 secondpoint = new Vector3(0f, 1f, -10f);
    protected Transform HouseTransform;
    protected bool RotateAroundPlayer = false;
    protected float RotationsSpeed = 1.0f;
    float boundsCenterY = -100f;
    float downTime = 0.5f;
    float timeLeft;


    void Start()
    {
        cam = Camera.main;
        timeLeft = 4 / Speed;

        if (HouseTransform != null)
        {
            Settings.instance.cameraOffset = cam.transform.position - HouseTransform.GetComponent<Renderer>().bounds.center;
        }

        buttonFlatName = GameObject.FindGameObjectWithTag("MPView");
        buttonFlatName.SetActive(false);

        imageCompas = GameObject.Find("ImageCompas").GetComponent<Image>();

        trMat = Resources.Load("TransparentMat", typeof(Material)) as Material;

        Settings.instance.flats = new List<Flat>();
        GameObject[] gameObjs = GameObject.FindGameObjectsWithTag("Flat");

        foreach (GameObject o in gameObjs)
        {
            Settings.instance.flats.Add(o.GetComponent<Flat>());
        }       
    }


    void Update()
    {
        if (Settings.instance.objectSelected)
        {
            HouseTransform = Settings.instance.House.transform;
            timeLeft -= Time.deltaTime;
            center = HouseTransform.GetComponent<Renderer>().bounds.center;

            if (timeLeft > 0)
            {
                Settings.instance.interactive = false;               
                cam.transform.position = Vector3.Lerp(cam.transform.position, new Vector3(center.x + 8, center.y + 2f, center.z), Speed * Time.deltaTime);
                cam.transform.LookAt(center);
                imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y - 105);
            }
            else
            {
                HouseTransform.gameObject.GetComponent<Renderer>().material = trMat;
                Settings.instance.objectSelected = false;
                Settings.instance.interactive = true;
                timeLeft = 2f;
                Settings.instance.cameraOffset = cam.transform.position - center;
                boundsCenterY = center.y;// + 10f;
                imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y - 105);
                Settings.instance.readyToOrbitRotate = true;
                Settings.instance.rotModeChanged = true;
                Popup.transform.SetPositionAndRotation(new Vector3(Screen.width / 2, Screen.height / 2, 0), Quaternion.identity);
                Popup.SetActive(true);

                GameObject.FindGameObjectWithTag("VillaInfo").GetComponent<Text>().text = Flat.VillaInfo(Settings.instance.selectedFlat);
                HouseTransform.Find("Point").GetComponent<Canvas>().gameObject.SetActive(false);

                const string selectedColorHexAv = "#26B4B74B";
                const string selectedColorHexRes = "#62615F4B";
                const string selectedColorHexSold = "#B70F004B";
                string selectedColorHex = selectedColorHexAv;
                Color selectedColor;

                switch (Settings.instance.selectedFlat.availability)
                {
                    case Flat.Availability.Available:
                        selectedColorHex = selectedColorHexAv;
                        break;
                    case Flat.Availability.Reserved:
                        selectedColorHex = selectedColorHexRes;
                        break;
                    case Flat.Availability.Sold:
                        selectedColorHex = selectedColorHexSold;
                        break;
                }

                buttonFlatName.SetActive(true);

                if (ColorUtility.TryParseHtmlString(selectedColorHex, out selectedColor))
                {
                    buttonFlatName.GetComponent<Image>().color = selectedColor;
                }

                GameObject.FindGameObjectWithTag("VillaName").GetComponent<Text>().text = HouseTransform.name;
                GameObject buttonSend = GameObject.Find("ButtonSend");
                buttonSend.SetActive(false);
            }
        }
    }


    void LateUpdate()
    {
        if (!Settings.IsPointerOverUIObject())
        {
            if (Settings.instance.readyToOrbitRotate && Settings.instance.interactive)
            {
                if (!Settings.instance.touchSupported)
                {
                    if (Input.GetMouseButtonDown(0)) RotateAroundPlayer = true;
                    if (Input.GetMouseButtonUp(0)) RotateAroundPlayer = false;

                    if (RotateAroundPlayer)
                    {
                        Quaternion camTurnAngle = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed * 3, Vector3.up);
                        float posY = cam.transform.position.y - Input.GetAxis("Mouse Y") * RotationsSpeed * 0.8f;
                        Settings.instance.cameraOffset = camTurnAngle * Settings.instance.cameraOffset;

                        newPos = HouseTransform.GetComponent<Renderer>().bounds.center + Settings.instance.cameraOffset;
                        newPos.y = Mathf.Clamp(posY, boundsCenterY + 3f, boundsCenterY + 18f);

                        cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, SmoothFactor);
                        cam.transform.LookAt(HouseTransform.GetComponent<Renderer>().bounds.center);

                        imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y - 105);
                    }
                }
                else
                {
                    if (Input.touchCount == 1)
                    {
                        if (Input.GetTouch(0).phase == TouchPhase.Began)
                        {
                            firstpoint = Input.GetTouch(0).position;
                        }
                        if (Input.GetTouch(0).phase == TouchPhase.Moved)
                        {
                            secondpoint = Input.GetTouch(0).position;

                            Quaternion camTurnAngle = Quaternion.AngleAxis((secondpoint.x - firstpoint.x) * RotationsSpeed / 250f, Vector3.up);

                            float posY = cam.transform.position.y - (secondpoint.y - firstpoint.y) * RotationsSpeed * 0.002f;
                            Settings.instance.cameraOffset = camTurnAngle * Settings.instance.cameraOffset;

                            Vector3 newPos = center + Settings.instance.cameraOffset;
                            newPos.y = Mathf.Clamp(posY, boundsCenterY + 3f, boundsCenterY + 8f);

                            cam.transform.position = Vector3.Lerp(cam.transform.position, newPos, SmoothFactor);
                            cam.transform.LookAt(center);

                            imageCompas.transform.eulerAngles = new Vector3(0, 0, cam.transform.eulerAngles.y - 105);
                        }
                    }
                }
            }

            if (!Settings.instance.orbitRotation && Settings.instance.interactive)
            {
                if (Settings.instance.readyToOrbitRotate)
                {
                    if (Input.GetAxis("Mouse ScrollWheel") != 0)
                    {
                        float scrollDistance = Input.GetAxis("Mouse ScrollWheel") * 2;

                        if (Vector3.Distance(cam.transform.position, center) - scrollDistance > 5
                            && Vector3.Distance(cam.transform.position, center) - scrollDistance < 25)
                        {
                            cam.transform.position = Vector3.MoveTowards(cam.transform.position, center, scrollDistance);
                            newPos = cam.transform.position;
                            Settings.instance.cameraOffset = cam.transform.position - center;
                            cam.transform.LookAt(center);
                        }
                    }
                }

                if (Input.touchCount == 2)
                {
                    Touch touchZero = Input.GetTouch(0);
                    Touch touchOne = Input.GetTouch(1);

                    Vector2 touchZeroPrePos = touchZero.position - touchZero.deltaPosition;
                    Vector2 touchOnePrePos = touchOne.position - touchOne.deltaPosition;

                    float prevMagnitude = (touchZeroPrePos - touchOnePrePos).magnitude;
                    float currentMagnitude = (touchZero.position - touchOne.position).magnitude;
                    float difference = (currentMagnitude - prevMagnitude) * SmoothFactor;

                    if (Vector3.Distance(cam.transform.position, center) - difference > 5
                      && Vector3.Distance(cam.transform.position, center) - difference < 15)
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