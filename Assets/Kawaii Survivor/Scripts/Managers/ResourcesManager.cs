using UnityEngine;

public class ResourcesManager
{
    const string statIconsDataPath = "Data/Stat Icons";
    const string objectDataPath = "Data/Objects/";
    const string weaponDataPath = "Data/Weapons/";

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

    public static ObjectDataSO GetRandomObject()
    {
        return Objects[Random.Range(0, objectData.Length)]; 
    }

    private static WeaponDataSO[] weaponData;
    public static WeaponDataSO[] Weapons
    {
        get
        {
            if (weaponData == null)
                weaponData = Resources.LoadAll<WeaponDataSO>(weaponDataPath);
            return weaponData;
        }
        private set { }
    }

    public static WeaponDataSO GetRandomWeapon()
    {
        return Weapons[Random.Range(0, Weapons.Length)];
    }
}
