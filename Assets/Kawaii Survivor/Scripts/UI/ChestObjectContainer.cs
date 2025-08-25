using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class ChestObjectContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Stats")]
    [SerializeField] private Transform statContainersParent;

    [field: SerializeField] public Button TakeButton { get; private set; }
    [field: SerializeField] public Button RecycleButton { get; private set; }

    [Header("Color")]
    [SerializeField] private Image[] levelDependentImages;
    [SerializeField] private Image outLine;


    public void Configure(ObjectDataSO objectData)
    {
        icon.sprite = objectData.Icon;
        nameText.text = objectData.Name;

        Color imageColor = ColorHolder.GetColor(objectData.Rarity);
        nameText.color = imageColor;

        outLine.color = ColorHolder.GetOutlineColor(objectData.Rarity);

        foreach (Image image in levelDependentImages)
            image.color = imageColor;

        ConfigureStatContainers(objectData.BaseStats);
    }

    private void ConfigureStatContainers(Dictionary<Stat, float> stats)
    {
        StatContainerManager.instance.GenerateStatContainers(stats, statContainersParent);
    }
}
