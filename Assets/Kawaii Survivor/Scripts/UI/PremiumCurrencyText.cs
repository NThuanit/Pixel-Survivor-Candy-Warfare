using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class PremiumCurrencyText : MonoBehaviour
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


