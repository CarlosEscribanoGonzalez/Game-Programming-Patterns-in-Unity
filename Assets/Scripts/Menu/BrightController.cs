using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightController : MonoBehaviour, ISaveableOption
{
    [SerializeField] private Image panel;
    [SerializeField] private Slider slider;

    public void UpdateBright()
    {
        float alpha = Mathf.Clamp(slider.value, 0.1f, 1);
        panel.color = new Color(panel.color.r, panel.color.g, panel.color.b, 1 - alpha);
    }

    public object GetData()
    {
        return new OptionsData
        {
            value = slider.value
        };
    }

    public void RestoreData(object data)
    {
        OptionsData oData = (OptionsData)data;
        slider.value = oData.value;
        UpdateBright();
    }
}
