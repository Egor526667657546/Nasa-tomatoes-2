using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons;
    [SerializeField] private GameObject gunPoint;
    public static Action<WeaponData> OnPickUpWeapon;

    private Animator animator;
    private WeaponData weaponData;
    private int cartridges;
    private bool hasGun = false;
    private bool canShoot = false;

    public bool HasGun { get => hasGun; set => hasGun = value; }
    private void Awake()
    {
        OnPickUpWeapon += PickUpWeapon;
        animator = gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && hasGun && canShoot)
        {
            Shoot();
        }
    }
    private void Shoot()
    {
        Debug.Log("Выстрел");
        if (weaponData == null) return;

        if (cartridges == 0)
        {
            canShoot = false;
            StartCoroutine(Reloading());
            return;
        }
        animator.SetTrigger("fire");
        Ray ray = new Ray(gunPoint.transform.position, gunPoint.transform.forward);
        RaycastHit hit;
        Debug.Log(weaponData.attackDistance);
        Debug.DrawRay(gunPoint.transform.position, transform.forward * weaponData.attackDistance, Color.red, 2f);
        if (Physics.Raycast(ray, out hit, weaponData.attackDistance))
        {
            GameObject possibleEnemy = hit.collider.gameObject;
            if (possibleEnemy.CompareTag("Enemy"))
            {
                possibleEnemy.GetComponent<EnemyBasic>().TakeDamage(weaponData.damage);
            }
        }
        cartridges--;
        canShoot = false;
        StartCoroutine(AttackDelay());
    }
    private IEnumerator Reloading()
    {
        Debug.Log("Перезарядка");
        yield return new WaitForSeconds(weaponData.reloadSpeed);
        this.cartridges = weaponData.cartridges;
        canShoot = true;
        Debug.Log("Перезарядка закончена");
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(weaponData.attackSpeed);
        canShoot = true;
    }
    private void PickUpWeapon(WeaponData weaponData)
    {
        canShoot = true;
        this.weaponData = weaponData;
        this.cartridges = weaponData.cartridges;
        foreach (var i in weapons)
        {
            if (i.gameObject.name == weaponData.type)
            {
                i.gameObject.SetActive(true);
                animator.SetBool($"have{weaponData.type}", true);
                Debug.Log(i.gameObject.activeSelf);
                Debug.Log(i.gameObject);
            }
        }
        hasGun = true;
    }
    private void OnDestroy()
    {
        OnPickUpWeapon -= PickUpWeapon;
    }
}
