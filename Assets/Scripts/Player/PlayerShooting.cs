using System;
using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public static Action<WeaponData> OnPickUpWeapon;

    private WeaponData weaponData;
    private int cartridges;
    private bool hasGun = false;
    private bool canShoot = false;

    public bool HasGun { get => hasGun; set => hasGun = value; }
    private void Awake()
    {
        OnPickUpWeapon += PickUpWeapon;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && hasGun && canShoot)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        if (weaponData == null) return;

        if (cartridges == 0)
        {
            StartCoroutine(Reloading());
            return;
        }
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject possibleEnemy = hit.rigidbody.gameObject;
            if (possibleEnemy.CompareTag("Enemy"))
            {
                //нанесення шкоди ворогу із weaponData;
            }
        }
        cartridges--;
        canShoot = false;
        StartCoroutine(AttackDelay());
    }
    private IEnumerator Reloading()
    {
        yield return new WaitForSeconds(weaponData.reloadSpeed);
        this.cartridges = weaponData.cartridges;
        canShoot = true;
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(weaponData.attackSpeed);
        canShoot = true;
    }
    private void PickUpWeapon(WeaponData weaponData)
    {
        this.weaponData = weaponData;
        this.cartridges = weaponData.cartridges;
        hasGun = true;
    }
    private void OnDestroy()
    {
        OnPickUpWeapon -= PickUpWeapon;
    }
}
