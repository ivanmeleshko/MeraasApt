using UnityEngine;
using UnityEngine.UI;

public class SquareSliderMax : MonoBehaviour
{

    [SerializeField]
    Slider sliderMax;
    [SerializeField]
    Image fillImage;
    public static float Value;
    const float coef = 0.145f;
    Text rangeText;  


    void Start()
    {
        rangeText = GameObject.FindGameObjectWithTag("SquareRangeText").GetComponent<Text>();
        Value = sliderMax.value;
    }


    public void OnValueChanged(float value)
    {
        if (value > SquareSliderMin.Value + 200f)
        {
            fillImage.rectTransform.sizeDelta = new Vector2((value - SquareSliderMin.Value) * coef, 28f);
            fillImage.rectTransform.anchoredPosition = new Vector2((SquareSliderMin.Value - 300) * coef + 5f + fillImage.rectTransform.sizeDelta.x / 2f, 0f);
            Value = value;
            rangeText.text = (int)(SquareSliderMin.Value / 12) + " - " + (int)(value / 12);
            Settings.instance.maxSurface = (int)(Value);
            Settings.instance.eventChanged = true;            
        }
        else
        {
            fillImage.rectTransform.sizeDelta = new Vector2(0, 28f);
            sliderMax.value = Value;
        }
    }
}
