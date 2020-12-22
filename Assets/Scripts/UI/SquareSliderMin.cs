using UnityEngine;
using UnityEngine.UI;

public class SquareSliderMin : MonoBehaviour
{

    [SerializeField]
    Slider sliderMin;
    [SerializeField]
    Image fillImage;
    public static float Value;
    const float coef = 0.145f;
    Text rangeText;


    void Start()
    {
        rangeText = GameObject.FindGameObjectWithTag("SquareRangeText").GetComponent<Text>();
        Value = sliderMin.value;
    }


    public void OnValueChanged(float value)
    {
        if (value < SquareSliderMax.Value - 200f)
        {
            fillImage.rectTransform.sizeDelta = new Vector2((SquareSliderMax.Value - value) * coef, 28f);
            fillImage.rectTransform.anchoredPosition = new Vector2((value - 300) * coef + 5f + fillImage.rectTransform.sizeDelta.x / 2f, 0f);
            Value = value;
            rangeText.text = (int)(value / 12) + " - " + (int)(SquareSliderMax.Value / 12);
            Settings.instance.minSurface = (int)(Value);
            Settings.instance.eventChanged = true;
        }
        else
        {
            fillImage.rectTransform.sizeDelta = new Vector2(0, 28f);
            sliderMin.value = Value;
        }
    }
}
