using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
public class WeaponSelectionContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI nameText;

    [Header("Stats")]
    [SerializeField] private Transform statContainersParent;

    [field: SerializeField] public Button Button { get; private set; }

    [Header("Color")]
    [SerializeField] private Image[] levelDependentImages;
    [SerializeField] private Image outLine;

   
    public void Configure(WeaponDataSO weaponData, int level)
    {
        icon.sprite = weaponData.Sprite;
        nameText.text = name + " (lvl " + (level + 1) + ")";

        Color imageColor = ColorHolder.GetColor(level);
        nameText.color = imageColor;

        outLine.color = ColorHolder.GetOutlineColor(level);

        foreach (Image image in levelDependentImages)
            image.color = imageColor;

        Dictionary<Stat, float> calculatedStats = WeaponStatsCalculator.GetStats(weaponData, level);
        ConfigureStatContainers(calculatedStats);
    }

    private void ConfigureStatContainers(Dictionary<Stat, float> calculatedStats)
    {
        StatContainerManager.instance.GenerateStatContainers(calculatedStats, statContainersParent);
    }

    public void Select()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one * 1.075f, 0.3f).setEase(LeanTweenType.easeOutSine);
    }

    public void Deselect()
    {
        LeanTween.cancel(gameObject);
        LeanTween.scale(gameObject, Vector3.one, 0.3f);
    }
}

