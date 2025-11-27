using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CharacterInfoPanel : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private GameObject priceContainer;
    [SerializeField] private Transform statsParent;

    [field: SerializeField] public Button Button { get; private set; }
    public void Configure(CharacterDataSO characterData, bool unlocked)
    {
        nameText.text = characterData.name;
        priceText.text = characterData.PurchasePrice.ToString();

        priceContainer.SetActive(!unlocked);

        StatContainerManager.instance.GenerateStatContainers(characterData.NonNeutralStats, statsParent);
    }
}
