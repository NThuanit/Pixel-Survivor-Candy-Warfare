using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemInfo : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI recyclePriceText;

    [Header("Colors")]
    [SerializeField] private Image container;

    [Header("Stats")]
    [SerializeField] private Transform statsParent;

    [Header("Buttons")]
    [field: SerializeField] public Button RecycleButton { get; private set; }
    [SerializeField] private Button mergeButton;

    public void Configure(Weapon weapon)
    {
        Configure(weapon.WeaponData.Sprite, 
            weapon.WeaponData.Name + "(lvl " + (weapon.Level + 1) + ")", 
            ColorHolder.GetColor(weapon.Level),
            WeaponStatsCalculator.GetRecyclePrice(weapon.WeaponData, weapon.Level), 
            weapon.WeaponData.BaseStats);

        mergeButton.gameObject.SetActive(true); 
    }

    public void Configure(ObjectDataSO objectData)
    {
        Configure(objectData.Icon, objectData.Name, ColorHolder.GetColor(objectData.Rarity), objectData.RecyclePrice, objectData.BaseStats);
        mergeButton.gameObject.SetActive(false);

    }


    private void Configure(Sprite itemIcon, string name, Color containerColor, int recyclePrice, Dictionary<Stat, float> stats)
    {
        icon.sprite = itemIcon;
        itemNameText.text = name;
        itemNameText.color = containerColor;

        recyclePriceText.text = recyclePrice.ToString();

        container.color = containerColor;

        StatContainerManager.instance.GenerateStatContainers(stats, statsParent);
    }    
}
