using UnityEngine;


public class UiDrag : MonoBehaviour
{

    [SerializeField]
    public GameObject Popup;
    private float offsetX;
    private float offsetY;  
    float clicked = 0;
    float clicktime = 0;
    float clickdelay = 0.5f;
    Vector3 curPos = new Vector3();


    public void BeginDrag()
    {
        offsetX = transform.position.x - Input.mousePosition.x;
        offsetY = transform.position.y - Input.mousePosition.y;
        Popup.transform.SetAsLastSibling();
    }


    public void OnDrag()
    {
        transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);

        if (Input.mousePosition.x < 100 || Input.mousePosition.y < 100 
            || Input.mousePosition.x > Screen.width - 100 || Input.mousePosition.y > Screen.height - 100)
        {
            Popup.SetActive(false);
        }
    }


    public void OnMouseDown()
    {
        Popup.transform.SetAsLastSibling();
    }


    public void ShowPopup()
    {
        Popup.SetActive(true);
        Popup.transform.SetAsLastSibling();
        if (Popup.name.Equals("PanelPopup"))
        {
            GameObject buttonSend = GameObject.Find("ButtonSend");
            buttonSend.SetActive(false);
        }
    }


    private bool DoubleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clicked++;
            if (clicked == 1) clicktime = Time.time;
        }
        if (clicked > 1 && Time.time - clicktime < clickdelay)
        {
            clicked = 0;
            clicktime = 0;
            return true;
        }
        else if (clicked > 2 || Time.time - clicktime > 1)
        {
            clicked = 0;
        }
        return false;
    }

}