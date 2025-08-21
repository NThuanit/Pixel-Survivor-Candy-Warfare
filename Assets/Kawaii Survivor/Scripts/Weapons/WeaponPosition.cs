using UnityEngine;

public class WeaponPosition : MonoBehaviour
{
    [Header("Elements")]
    public Weapon Weapon { get; private set; }

   

    public void AssignWeapon(Weapon weapon, int weaponLevel)
    {
        Weapon = Instantiate(weapon, transform);

        Weapon.transform.localPosition = Vector3.one;
        Weapon.transform.localRotation = Quaternion.identity;
        
        Weapon.UpgradeTo(weaponLevel);
    }
}
