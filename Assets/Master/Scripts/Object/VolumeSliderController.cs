using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSliderController : MonoBehaviour
{
    [SerializeField] private Slider m_SliderVolume;
    [SerializeField] private TextMeshProUGUI m_TextValue;
    public static Action<float> OnTimeSliderValueChanged;
    private void Awake()
    {
        m_SliderVolume.onValueChanged.AddListener(OnSliderValueChanged);
        OnSliderValueChanged(m_SliderVolume.value);
    }

    private void OnSliderValueChanged(float value)
    {
        float step = 0.1f;
        float roundedValue = Mathf.Round(value / step) * step;
        m_SliderVolume.SetValueWithoutNotify(roundedValue);
        m_TextValue.text = Config.Slider_Value + roundedValue + Config.Unit_Time;
        OnTimeSliderValueChanged?.Invoke(value);
    }
}

