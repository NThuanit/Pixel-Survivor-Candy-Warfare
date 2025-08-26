using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;

public class StatContainerManager : MonoBehaviour
{
    public static StatContainerManager instance;

    [Header("Elements")]
    [SerializeField] private StatContainer statContainer;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else 
            Destroy(gameObject);
    }

    private void GenerateContainers(Dictionary<Stat, float> statDicitonary, Transform parent)
    {
        List<StatContainer> statContainers = new List<StatContainer>();


        foreach (KeyValuePair<Stat, float> kvp in statDicitonary)
        {
            StatContainer containerInstance = Instantiate(statContainer, parent);

            Sprite icon = ResourcesManager.GetStatIcon(kvp.Key);
            string statName = Enums.FormatStatName(kvp.Key);
            float statValue = kvp.Value;
            containerInstance.Configure(icon, statName, statValue);
        }

        LeanTween.delayedCall(Time.deltaTime * 2, () => ResizeTexts(statContainers));
    }

    public void ResizeTexts(List<StatContainer> statContainers)
    {
        float minFontSize = 5000;

        for (int i = 0; i < statContainers.Count; i++)
        {
            StatContainer statContainer = statContainers[i];
            float fonSize = statContainer.GetFontSize();

            if  (fonSize < minFontSize)
                minFontSize = fonSize;
        }

        for (int i = 0; i < statContainers.Count; i++)
            statContainers[i].SetFontSize(minFontSize);
    }

    public void GenerateStatContainers(Dictionary<Stat, float> statDictionary, Transform parent)
    {
        instance.GenerateContainers(statDictionary, parent);    
    }
}
  