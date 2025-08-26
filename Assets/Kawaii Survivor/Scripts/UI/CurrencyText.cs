using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CurrencyText : MonoBehaviour
{
    [Header("Elementsd")]
    private TextMeshProUGUI text;

    public void UpdateText(string currencyString)
    {
        if (text == null)
            text = GetComponent<TextMeshProUGUI>();
        text.text = currencyString;
    }
}
