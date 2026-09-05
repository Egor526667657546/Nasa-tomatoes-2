using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private UIManager UIManager;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private GameObject akImageRight;
    [SerializeField] private GameObject akImageDown;
    [SerializeField] private GameObject laserImageRight;
    [SerializeField] private GameObject laserImageDown;
    [SerializeField] private GameObject pistolImage;
    private WeaponData weaponUp; //pistolet
    private WeaponData weaponRight; //rifle (AK47 or Laser)
    private WeaponData weaponDown; //rifle2

    private bool haveWeapon = false;
    private bool rightTaken = false;
    private bool downTaken = false;
    public static Action<WeaponData> OnPickUpWeapon;

    private void Awake()
    {
        OnPickUpWeapon += PickUpWeapon;
    }

    private void PickUpWeapon(WeaponData weapon)
    {
        if (weapon.type == "Rifle")
        {
            if (!rightTaken)
            {
                rightTaken = true;
                weaponRight = weapon;
                switch (weapon.idName)
                {
                    case "AK":
                        akImageRight.SetActive(true);
                        break;
                    case "Laser":
                        laserImageRight.SetActive(true);
                        break;
                    default:
                        Debug.Log("How");
                        break;
                }
              
            }
            else if (!downTaken)
            {
                downTaken = true;
                weaponDown = weapon;
                switch (weapon.idName)
                {
                    case "AK":
                        akImageDown.SetActive(true);
                        break;
                    case "Laser":
                        laserImageDown.SetActive(true);
                        break;
                    default:
                        Debug.Log("How");
                        break;
                }
            }
           
        }
        else if (weapon.type == "Pistol")
        {
            weaponUp = weapon;
            pistolImage.SetActive(true);
        }

        playerShooting.Types.Clear();

        if (weaponUp != null)
        {
            playerShooting.Types.Add(weaponUp.type);
        }

        if (weaponRight != null)
        {
            playerShooting.Types.Add(weaponRight.type);
        }

        if (weaponDown != null)
        {
            playerShooting.Types.Add(weaponDown.type);
        }

        if (!haveWeapon)
        {
            UIManager.ChangeCrosshairs(0, 1);
            haveWeapon = true;
            playerShooting.EquipWeapon(weapon);
        }
    }

    public void ChangeWeapon(int number)
    {
        switch (number)
        {
            case 0:
                if (weaponUp != null)
                {
                    playerShooting.EquipWeapon(weaponUp);
                    Debug.Log("changed to pistol!");
                }
                break;

            case 1:
                if (weaponRight != null)
                {
                    playerShooting.EquipWeapon(weaponRight);
                    Debug.Log("changed to ak or laser!");
                }
                break;
            case 2:
                if (weaponDown != null)
                {
                    playerShooting.EquipWeapon(weaponDown);
                    Debug.Log("changed to ak or laser!");
                }
                break;
            default:
                Debug.Log("changed to nothing!");
                break;
        }
    }
    private void OnDestroy()
    {
        OnPickUpWeapon -= PickUpWeapon;
    }
}
