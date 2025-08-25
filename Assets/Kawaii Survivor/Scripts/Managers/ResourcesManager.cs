using UnityEngine;

public class ResourcesManager
{
    const string statIconsDataPath = "Data/Stat Icons";
    const string objectDataPath = "Data/Objects/";

    private static StatIcon[] statIcons; 
    public static Sprite GetStatIcon(Stat stat)
    {
        if (statIcons == null)
        {
            StatIconDataSO data = Resources.Load<StatIconDataSO>(statIconsDataPath);
            statIcons = data.StatIcons;
        }

        foreach (StatIcon statIcon in statIcons)
        {
            if (stat == statIcon.stat)
            {
                //Debug.Log(statIcon.icon.name + " !");
                return statIcon.icon;
            }
        }

        Debug.LogError("No icon found for stat : " + stat);

        return null;
    }

    private static ObjectDataSO[] objectData;
    public static ObjectDataSO[] Objects
    {
        get 
        { 
            if (objectData == null)
                objectData = Resources.LoadAll<ObjectDataSO>(objectDataPath); 
            return objectData;
        }
        private set { }
    }
}
