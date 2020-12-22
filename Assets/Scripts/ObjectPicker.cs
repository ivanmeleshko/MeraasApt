using UnityEngine;
using UnityEngine.UI;


public class ObjectPicker : MonoBehaviour
{

    Camera cam;
    Slider floorSlider;
    Transform floor4;


    private void Start()
    {
        cam = Camera.main;
        floorSlider = GameObject.Find("SliderFloor").GetComponent<Slider>();
        floor4 = GameObject.Find("Floor4").GetComponent<Transform>();
    }


    public void SelectObject()
    {
        if (Settings.instance.interactive)
        {
            ClosePopups();

            if (Settings.instance.House != null)
            {
                Settings.instance.House.transform.Find("Point").GetComponent<Canvas>().gameObject.SetActive(true);
                Settings.instance.House.gameObject.GetComponent<Renderer>().material = Flat.ChangeMaterial(Settings.instance.selectedFlat.availability);
            }

            Settings.instance.House = gameObject;

            if (Settings.instance.House.transform != null && !floor4.gameObject.active)
            {
                floorSlider.gameObject.SetActive(false);
                Settings.instance.objectSelected = true;
                Settings.instance.selectedFlat = new Flat();
                Settings.instance.selectedFlat = gameObject.GetComponent<Flat>();
                Settings.instance.orbitRotation = false;
                Settings.instance.interactive = false;
            }
        }
    }


    private void ClosePopups()
    {
        GameObject[] popups = GameObject.FindGameObjectsWithTag("Popup");
        foreach (GameObject p in popups)
        {
            p.SetActive(false);
        }
    }
}
