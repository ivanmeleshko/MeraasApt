using UnityEngine;
using UnityEngine.UI;


public class BudgetSliderMax : MonoBehaviour
{

    [SerializeField]
    Slider sliderMax;
    [SerializeField]
    Image fillImage;
    public static float Value;
    const float coef = 64f;
    Text rangeText;


    void Start()
    {
        rangeText = GameObject.FindGameObjectWithTag("BudgetRangeText").GetComponent<Text>();
        Value = sliderMax.value;
    }


    public void OnValueChanged(float value)
    {
        if (value > BudgetSliderMin.Value)
        {
            fillImage.rectTransform.sizeDelta = new Vector2((value - BudgetSliderMin.Value) * coef, 28f);
            fillImage.rectTransform.anchoredPosition = new Vector2(BudgetSliderMin.Value * coef + 5f + fillImage.rectTransform.sizeDelta.x / 2f, 0f);
            Value = value;
            rangeText.text = BudgetSliderMin.Value + "M - " + value + "M";
            Settings.instance.maxPrice = (int)(Value * 1000000);
            Settings.instance.eventChanged = true;
        }
        else
        {
            fillImage.rectTransform.sizeDelta = new Vector2(0, 28f);
            sliderMax.value = Value;
        }
    }
}