using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CustomSlider : MonoBehaviour
{
    [SerializeField] private Slider m_slider;
    [SerializeField] private TextMeshProUGUI m_value;
    [SerializeField] private TextMeshProUGUI m_minValue;
    [SerializeField] private TextMeshProUGUI m_maxValue;

    private string m_format = "F0";

    public void Setup(float min, float max)
    {
        m_slider.minValue = min;
        m_slider.maxValue = max;
        m_minValue.text = min.ToString(m_format);
        m_maxValue.text = max.ToString(m_format);
    }

    // Update is called once per frame
    void Update()
    {
        m_value.text = m_slider.value.ToString(m_format);
    }

    public float GetValue()
    {
        return m_slider.value;
    }
}
