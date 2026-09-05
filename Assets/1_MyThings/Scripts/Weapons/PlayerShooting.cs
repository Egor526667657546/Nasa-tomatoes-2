using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
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

    private bool canCount = false;
    private float shotCooldownTimer = 0f;

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
        //Time.timeScale = 0.1f;
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

        // Debug.Log($"can shoot: {canShoot}");


















        //if (Input.GetMouseButton(0) && hasGun && isAiming && canShoot)
        //{
        //    Shoot();
        //}
        //else if (Input.GetMouseButtonUp(0) || !hasGun || !isAiming)
        //{
        //    StartCoroutine(DelayBeforeQuitting());
        //}
        //if (Input.GetMouseButtonDown(0) && hasGun && isAiming)
        //{
        //    newTime = 1;
        //}
        //if (Input.GetMouseButton(0) && hasGun && isAiming)
        //{
        //    if (newTime >= 0.1f)
        //    {
        //        NormalShooting();
        //    }
        //}
        //if (canCount)
        //{
        //    Waiting();
        //}
        if (shotCooldownTimer > 0)
        {
            shotCooldownTimer -= Time.deltaTime;
        }

        if (hasGun && isAiming)
        {
            if (Input.GetMouseButton(0) && shotCooldownTimer <= 0)
            {
                NormalShooting();
            }
            if (Input.GetMouseButtonUp(0))
            {
                animator.SetBool("isShooting", false);
            }
        }
        else
        {

            animator.SetBool("isShooting", false);
        }
    }
    //private void Shoot()
    //{
    //    Debug.Log(cartridges);
    //    animator.SetBool("isShooting", true);
    //    StartCoroutine(AttackAnimDelay());
    //    if (weaponData == null) return;

    //    if (cartridges == 0)
    //    {
    //        animator.SetBool("isShooting", false);
    //        canShoot = false;
    //        return;
    //    }
    //    Ray ray = new Ray(gunPoint.transform.position, gunPoint.transform.forward);
    //    RaycastHit hit;
    //    Debug.DrawRay(gunPoint.transform.position, gunPoint.transform.forward, Color.red, 2f);
    //    if (Physics.Raycast(ray, out hit, weaponData.attackDistance))
    //    {
    //        GameObject possibleEnemy = hit.collider.gameObject;
    //        if (possibleEnemy.CompareTag("Enemy"))
    //        {
    //            possibleEnemy.GetComponent<EnemyBasic>().TakeDamage(weaponData.damage);
    //        }
    //    }
    //    cartridges--;
    //    StartCoroutine(AttackDelay());
    //}
    //private void Shoot()
    //{
    //    //Debug.Log("Shoot called");
    //    shootCalled += 1;
    //    //Debug.Log($"For: {shootCalled} time");
    //    Debug.Log($"cartriges: {cartridges - 1}");
    //    canShoot = false;
    //  //  Debug.Log($"can shoot: {canShoot}");
    //    if (weaponData == null)
    //        return;

    //    if (cartridges == 0)
    //    {
    //        animator.SetBool("isShooting", false);
    //        return;
    //    }

    //    animator.SetBool("isShooting", true);
    //    Fire();
    ////    Debug.Log($"animator.GetBool(isShooting): {animator.GetBool("isShooting")}");

    //    StartCoroutine(AttackAnimDelay());
    //    StartCoroutine(AttackDelay());
    //}
    //private void NormalShooting()
    //{
    //    newTime = 0f;
    //    canCount = true;

    //    if (weaponData == null)
    //        return;

    //    if (cartridges == 0)
    //    {
    //        animator.SetBool("isShooting", false);
    //        return;
    //    }

    //    Debug.Log($"cartriges: {cartridges - 1}");
    //    animator.SetBool("isShooting", true);
    //    Fire();
    //    StartCoroutine(AttackAnimDelay());
    //}
    private void NormalShooting()
    {
        if (weaponData == null) return;

        if (cartridges <= 0)
        {
            animator.SetBool("isShooting", false);
            return;
        }

        shotCooldownTimer = weaponData.attackSpeed;

        Debug.Log($"cartridges: {cartridges - 1}");

        animator.SetBool("isShooting", true);

        Fire();
    }
    public void Fire()
    {
      //  Debug.Log($"Fire called");
        //FireCalled += 1;
      //  Debug.Log($"Fire called for {FireCalled} time");
        if (weaponData == null)
            return;

        Ray ray = new Ray(gunPoint.transform.position, gunPoint.transform.forward);
        Debug.DrawRay(gunPoint.transform.position, gunPoint.transform.forward, Color.red, 2f);
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
    //private IEnumerator AttackAnimDelay()
    //{
    //    yield return new WaitForSeconds(shotTime);
    //    animator.SetBool("isShooting", false);
    //}
    private IEnumerator AttackDelay()
    {
      //  Debug.Log($"started coroutine AttackDelay");
        Debug.Log($"wait time: {weaponData.attackSpeed}");
        Debug.Log("waiting started");
        canCount = true;
        yield return new WaitForSeconds(weaponData.attackSpeed);
        canCount = false;
        animator.SetBool("isShooting", false);
      //  Debug.Log($"animator.GetBool(isShooting): {animator.GetBool("isShooting")}");

        canShoot = true;
    }
    //private void Waiting()
    //{
    //    newTime += Time.deltaTime;
    //    //Debug.Log($"time waited: {newTime}");
    //}
    //private IEnumerator DelayBeforeQuitting()
    //{
    //    yield return new WaitForSeconds(shotTime - curTime);
    //    animator.SetBool("isShooting", false);
    //    curTime = 0f;
    //}
    public void EquipWeapon(WeaponData weaponData)
    {
        hasGun = true;
        canShoot = true;
        this.weaponData = weaponData;
        this.cartridges = weaponData.cartridges;
        foreach (var i in PlayerWeapons)
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