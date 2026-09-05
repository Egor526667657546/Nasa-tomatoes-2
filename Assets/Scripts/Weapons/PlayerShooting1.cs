using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting1 : MonoBehaviour
{
    [SerializeField] private UIManager UIManager;
    [SerializeField] private Inventory inventory;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private List<GameObject> playerWeapons;
    [SerializeField] private GameObject gunPoint;


    private Animator animator;
    private WeaponData weaponData;
    private GameObject usingWeapon;
    private List<string> types;

    private int cartridges;
    private bool hasGun = false;
    private bool isAiming = false;
    private bool isReloading = false;
    private bool canShoot = false;

    private float curTime = 0f;
    private float shotTime = 0.03f;

    public bool HasGun { get => hasGun; set => hasGun = value; }

    public List<GameObject> PlayerWeapons { get => playerWeapons; set => playerWeapons = value; }
    public List<string> Types { get => types; set => types = value; }

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
        types = new List<string>();
    }

    private void Update()
    {
        isAiming = false;
        if (Input.GetKeyDown(KeyCode.R) && hasGun)
        {
            if (!isReloading)
            {
                canShoot = false;
                isReloading = true;
                StartCoroutine(Reloading());
            }
        }

        if (Input.GetMouseButton(1) && hasGun)
        {
            isAiming = true;

            cameraMove.CamRotCeiling = -10;
            cameraMove.CamRotFloor = 10;

            UIManager.ChangeCrosshairs(1, 2);
        }
        else if (Input.GetMouseButtonUp(1) && hasGun)
        {

            cameraMove.CamRotCeiling = -30f;
            cameraMove.CamRotFloor = 55f;

            UIManager.ChangeCrosshairs(2, 1);

        }
        animator.SetBool("isAiming", isAiming);

        if (Input.GetMouseButton(0) && hasGun && isAiming && canShoot)
        {
            Shoot();
        }
        else if (Input.GetMouseButtonUp(0) || !hasGun || !isAiming)
        {
            StartCoroutine(DelayBeforeQuitting());
        }

        if (animator.GetBool("isShooting"))
        {
            curTime += Time.deltaTime;
            if (curTime >= shotTime)
            {
                curTime = 0f;
            }
        }
    }
    private void Shoot()
    {
        Debug.Log(cartridges);
        if (weaponData == null) return;

        if (cartridges == 0)
        {
            animator.SetBool("isShooting", false);
            canShoot = false;
            return;
        }

        animator.SetBool("isShooting", true);
        StartCoroutine(AttackAnimDelay());
        Ray ray = new Ray(gunPoint.transform.position, gunPoint.transform.forward);
        RaycastHit hit;
        Debug.DrawRay(gunPoint.transform.position, gunPoint.transform.forward, Color.red, 2f);
        if (Physics.Raycast(ray, out hit, weaponData.attackDistance))
        {
            GameObject possibleEnemy = hit.collider.gameObject;
            if (possibleEnemy.CompareTag("Enemy"))
            {
                possibleEnemy.GetComponent<EnemyBasic>().TakeDamage(weaponData.damage);
            }
        }
        cartridges--;
        StartCoroutine(AttackDelay());
    }
    private IEnumerator Reloading()
    {
        Debug.Log("Перезарядка");
        animator.SetBool("reload", true);
        canShoot = false;
        yield return new WaitForSeconds(weaponData.reloadSpeed);
        Debug.Log("Перезарядка закончена");
        animator.SetBool("reload", false);
        this.cartridges = weaponData.cartridges;
        canShoot = true;
        isReloading = false;

    }
    private IEnumerator AttackAnimDelay()
    {
        yield return new WaitForSeconds(shotTime);
        animator.SetBool("isShooting", false);
    }
    private IEnumerator AttackDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(weaponData.attackSpeed);
        canShoot = true;
    }
    private IEnumerator DelayBeforeQuitting()
    {
        Debug.Log(shotTime - curTime);
        yield return new WaitForSeconds(shotTime - curTime);
        animator.SetBool("isShooting", false);
        curTime = 0f;
    }
    public void EquipWeapon(WeaponData weaponData)
    {
        hasGun = true;
        canShoot = true;
        this.weaponData = weaponData;
        this.cartridges = weaponData.cartridges;
        foreach (var i in playerWeapons)
        {
            if (i.gameObject.name == weaponData.idName)
            {
                usingWeapon = i.gameObject;
                i.gameObject.SetActive(true);
                animator.SetBool($"have{weaponData.type}", true);
            }
            else
            {
                i.gameObject.SetActive(false);
            }
        }
        foreach (var i in types)
        {
            if (i != this.weaponData.type)
            {
                animator.SetBool($"have{i}", false);
            }
        }
    }
}
