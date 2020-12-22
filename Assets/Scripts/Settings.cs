using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Settings : MonoBehaviour
{

    public GameObject House;
    public List<Flat> flats, displayedFlats;
    public Flat selectedFlat;
    public int minPrice, maxPrice = 5000000;
    public int minSurface = 300, maxSurface = 2500;
    public bool available = true, reserved, sold;
    public bool townHouse = true, twinhome, luxury;
    public bool eventChanged;
    public bool rotModeChanged;
    public bool interactive = true;
    public bool objectSelected = false;
    public bool readyToOrbitRotate = false;
    public bool orbitRotation = true;
    public bool touchSupported = false;
    public string previousScene;
    public string currentScene;
    public Vector3 cameraOffset;
    public static Settings instance = null;


    private void Awake()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            touchSupported = true;
        }

        if (instance == null)
        {         
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }


    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
