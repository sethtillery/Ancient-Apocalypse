using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform weaponObjectsContainer;
    [SerializeField] weaponData startingWeapon;

    private void Start()
    {
        addWeapon(startingWeapon);
    }

    public void addWeapon(weaponData weaponData)
    {
        GameObject weaponGameObject = Instantiate(weaponData.weaponBasePrefab, weaponObjectsContainer);
        weaponGameObject.GetComponent<WeaponBase>().SetData(weaponData);

        Level level = GetComponent<Level>();
        if (level != null)
            level.AddUpgradesToAvailableUpgrades(weaponData.upgrades);
    }
}
