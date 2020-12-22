using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{

    [SerializeField]
    GameObject Popup;
    [SerializeField]
    bool buttonStateActive;
    [SerializeField]
    GameObject[] Links;
    [SerializeField]
    public Button button;
    const string selectedColorHex = "#C51844FF";
    const string selectedColorHexAv = "#26B4B7FF";
    const string selectedColorHexRes = "#62615FFF";
    const string selectedColorHexSold = "#B70F00FF";
    const string normalColorHex = "#6A1328FF";
    private Color selectedColor;
    private Color normalColor;
    private List<Flat> villas;
    List<Flat> displayedVillas;  
    GameObject[] links;
    float sensitivity = 5f;


    public void ShowPopup()
    {
        if (!Popup.activeInHierarchy)
        {
            switch (Popup.name)
            {
                case "PanelFloorPlan":
                    Popup.transform.SetPositionAndRotation(new Vector3(Screen.width / 3.5f, Screen.height / 2.5f, 0), Quaternion.identity);
                    break;
                case "PanelPhoto":
                    Popup.transform.SetPositionAndRotation(new Vector3(Screen.width / 1.4f, Screen.height / 2.5f, 0), Quaternion.identity);

                    break;
                case "PanelVR360":
                    Popup.transform.SetPositionAndRotation(new Vector3(Screen.width / 1.35f, Screen.height / 1.4f, 0), Quaternion.identity);
                    break;
                case "PanelStreetView":
                    Popup.transform.SetPositionAndRotation(new Vector3(Screen.width / 3.8f, Screen.height / 1.4f, 0), Quaternion.identity);
                    break;
            }
            Popup.SetActive(true);
            Popup.transform.SetAsLastSibling();
            Popup.GetComponent<RectTransform>().sizeDelta = new Vector2(1000, 600); //new Vector2(Screen.width / 2, Screen.height / 2);       
        }
    }


    public void ClosePopup()
    {
        Popup.SetActive(false);
        GameObject[] popups = GameObject.FindGameObjectsWithTag("Popup");
        GameObject backGround = GameObject.FindGameObjectWithTag("Background");

        foreach (GameObject p in popups)
        {
            if (p.active)
            {
                backGround.SetActive(true);
                break;
            }
        }
    }


    public void ChangeState()
    {
        buttonStateActive = !buttonStateActive;
        Settings.instance.eventChanged = true;

        if (buttonStateActive)
        {
            switch (button.name)
            {
                case "ButtonAvailable":
                    if (ColorUtility.TryParseHtmlString(selectedColorHexAv, out selectedColor))
                    {
                        button.GetComponent<Image>().color = selectedColor;
                    }
                    break;
                case "ButtonReserved":
                    if (ColorUtility.TryParseHtmlString(selectedColorHexRes, out selectedColor))
                    {
                        button.GetComponent<Image>().color = selectedColor;
                    }
                    break;
                default:
                    if (ColorUtility.TryParseHtmlString(selectedColorHex, out selectedColor))
                    {
                        button.GetComponent<Image>().color = selectedColor;
                    }
                    break;
            }

            switch (button.name)
            {
                case "ButtonAvailable":
                    Settings.instance.available = true;
                    break;
                case "ButtonReserved":
                    Settings.instance.reserved = true;
                    break;
                case "ButtonSold":
                    Settings.instance.sold = true;
                    break;
                case "ButtonTownHouse":
                    Settings.instance.townHouse = true;
                    break;
                case "ButtonTwinHome":
                    Settings.instance.twinhome = true;
                    break;
                case "ButtonLuxury":
                    Settings.instance.luxury = true;
                    break;
            }
        }
        else
        {
            if (ColorUtility.TryParseHtmlString(normalColorHex, out normalColor))
            {
                button.GetComponent<Image>().color = normalColor;
            }

            switch (button.name)
            {
                case "ButtonAvailable":
                    Settings.instance.available = false;
                    break;
                case "ButtonReserved":
                    Settings.instance.reserved = false;
                    break;
                case "ButtonSold":
                    Settings.instance.sold = false;
                    break;
                case "ButtonTownHouse":
                    Settings.instance.townHouse = false;
                    break;
                case "ButtonTwinHome":
                    Settings.instance.twinhome = false;
                    break;
                case "ButtonLuxury":
                    Settings.instance.luxury = false;
                    break;
            }
        }
    }
}
