using UnityEngine;

public class PlayerWeapons : MonoBehaviour
{
    [Header("Element")]
    [SerializeField] private WeaponPosition[] weaponPositions;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddWeapon(WeaponDataSO selectedWeapon, int weaponLevel)
    {
        //Instantiate(selectedWeapon.Prefab, weaponsParent);
        weaponPositions[Random.Range(0, weaponPositions.Length)].AssignWeapon(selectedWeapon.Prefab, weaponLevel);
    }
}
