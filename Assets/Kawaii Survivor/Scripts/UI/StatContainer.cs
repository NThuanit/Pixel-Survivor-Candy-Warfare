using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatContainer : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private Image statIcon;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private TextMeshProUGUI statValueText;

    public void Configure(Sprite icon, string statName, string statValue)
    {
        statIcon.sprite = icon;
        statText.text = statName;
        statValueText.text  = statValue;   
    }

    public float GetFontSize()
    {
        return statText.fontSize;
    }

    public void SetFontSize(float fontSize)
    {
        statText.fontSizeMax = fontSize;
        statValueText.fontSizeMax = fontSize;
    }
}
