using UnityEngine;
using UnityEngine.UI;

public class BudgetSliderMin : MonoBehaviour
{

    [SerializeField]
    Slider sliderMin;
    [SerializeField]
    Image fillImage;
    public static float Value;
    const float coef = 64f;
    Text rangeText;


    void Start()
    {
        rangeText = GameObject.FindGameObjectWithTag("BudgetRangeText").GetComponent<Text>();
        Value = sliderMin.value;
    }


    public void OnValueChanged(float value)
    {
        if (value < BudgetSliderMax.Value)
        {
            fillImage.rectTransform.sizeDelta = new Vector2((BudgetSliderMax.Value - value) * coef, 28f);
            fillImage.rectTransform.anchoredPosition = new Vector2(value * coef + 5f + fillImage.rectTransform.sizeDelta.x / 2f, 0f);
            Value = value;
            rangeText.text = value + "M - " + BudgetSliderMax.Value + "M";
            Settings.instance.minPrice = (int)(Value * 1000000);
            Settings.instance.eventChanged = true;
        }
        else
        {
            fillImage.rectTransform.sizeDelta = new Vector2(0, 28f);
            sliderMin.value = Value;
        }
    }
}
