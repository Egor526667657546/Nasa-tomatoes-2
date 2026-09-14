using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Movement1 movement;
    [SerializeField] private Inventory inventory;
    [SerializeField] private WeaponRecoil weaponRecoil;
    [SerializeField] private Spread spread;
    [SerializeField] private CameraMove cameraMove;
    [SerializeField] private List<GameObject> playerWeapons;
    [SerializeField] private GameObject gunPoint;
    [SerializeField] private Image AmmoCircle;
    [SerializeField] private Image AmmoCircleMask;
    [SerializeField] private TextMeshProUGUI ammoText;


    private Animator animator;
    private WeaponData weaponData;
    private GameObject usingWeapon;
    private List<string> types;

    private int pistolCartriges = -1;
    private int akCartriges = -1;
    private int laserCartriges = -1;

    private int cartridges;
    private bool hasGun = false;
    private bool isAiming = false;
    private bool isReloading = false;

    private bool wasAimingWithGun;
    private bool ammoActivated = false;
    private bool inSomething = false;
    private float shotCooldownTimer = 0f;
    private float N = 0f;

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
        //Time.timeScale = 0.2f;
        isAiming = false;
        if (Input.GetKeyDown(KeyCode.R) && hasGun)
        {
            if (!isReloading)
            {
                StartCoroutine(ReloadAmmoTextAnim());
                StartCoroutine(Reloading());
            }
        }

        if (Input.GetMouseButton(1) && hasGun)
        {
            isAiming = true;

            //cameraMove.CamRotCeiling = -10;
            //cameraMove.CamRotFloor = 10;

            uiManager.ChangeCrosshairs(1, 2);
        }
        else if (Input.GetMouseButtonUp(1) && hasGun)
        {

            //cameraMove.CamRotCeiling = -30f;
            //cameraMove.CamRotFloor = 55f;

            uiManager.ChangeCrosshairs(2, 1);

        }

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

        bool aimingWithGunNow = hasGun && isAiming;

        if (aimingWithGunNow)
        {
            if (weaponData.type == "Rifle")
            {
                if (Input.GetMouseButton(0) && shotCooldownTimer <= 0 && !isReloading)
                    RifleShooting();
                else if (Input.GetMouseButtonUp(0) || movement.MovementType == 2)
                    StartCoroutine(DelayBeforeQuitting(false));
            }
            else if (weaponData.type == "Pistol")
            {
                if (Input.GetMouseButtonDown(0) && shotCooldownTimer <= 0 && !isReloading)
                    PistolShooting();
            }
        }
        else if (wasAimingWithGun && usingWeapon != null)
        {
            StartCoroutine(DelayBeforeQuitting(true));
        }

        wasAimingWithGun = aimingWithGunNow;

        if (weaponData != null)
        {
            if (!ammoActivated)
            {
                ammoActivated = true;
                uiManager.ShowAmmo();
            }
            UpdateAmmo();
        }
    }
    private void LateUpdate()
    {
        animator.SetBool("isAiming", isAiming);
        if (inSomething)
        {
            animator.SetBool($"in{weaponData.type}", true);
            inSomething = false;
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
    private void RifleShooting()
    {
        if (weaponData == null) return;

        if (cartridges <= 0)
        {
            animator.SetBool("isShooting", false);
            return;
        }

        shotCooldownTimer = weaponData.attackSpeed;

        //Debug.Log($"cartridges: {cartridges - 1}");

        Fire();
        animator.SetBool("isShooting", true);
    }
    private void PistolShooting()
    {
        if (weaponData == null) return;

        if (cartridges <= 0)
        {
            animator.SetBool("isShooting", false);
            return;
        }

        shotCooldownTimer = weaponData.attackSpeed;

        //Debug.Log($"cartridges: {cartridges - 1}");

        animator.SetBool("isShooting", true);
        Fire();
        StartCoroutine(AttackAnimDelay());
    }
    //public void Fire()
    //{
    //    //  Debug.Log($"Fire called");
    //    //FireCalled += 1;
    //    //  Debug.Log($"Fire called for {FireCalled} time");
    //    if (weaponData == null)
    //        return;

    //    Quaternion spreadRotation = spread.CalculateSpread(movement.MovementType);
    //    Quaternion spraySpread = weaponRecoil.GetSpray();
    //    Quaternion finalSpread = spreadRotation * spraySpread;
    //    //Vector3 finalDirection = finalSpread * gunPoint.transform.forward;
    //    Vector3 finalDirection = finalSpread * Camera.main.transform.forward;

    //    Ray ray = new Ray(gunPoint.transform.position, finalDirection);
    //    RaycastHit hit;

    //    Debug.DrawRay(gunPoint.transform.position, finalDirection * weaponData.attackDistance, Color.red, 2f);

    //    if (Physics.Raycast(ray, out hit, weaponData.attackDistance))
    //    {
    //        GameObject possibleEnemy = hit.collider.gameObject;

    //        if (possibleEnemy.CompareTag("Enemy"))
    //        {
    //            possibleEnemy.GetComponent<EnemyBasic>().TakeDamage(weaponData.damage);
    //        }
    //    }

    //    cartridges--;
    //}
    //public void Fire()
    //{
    //    //  Debug.Log($"Fire called");
    //    //FireCalled += 1;
    //    //  Debug.Log($"Fire called for {FireCalled} time");
    //    if (weaponData == null)
    //        return;

    //    //Quaternion spreadRotation = spread.CalculateSpread(movement.MovementType);
    //    weaponRecoil.GetSpray();
    //    Vector3 crosshairScreenPos = RectTransformUtility.WorldToScreenPoint(null, crosshairImage.rectTransform.position);
    //    //Quaternion finalSpread = spreadRotation * spraySpread;
    //    //Vector3 finalDirection = finalSpread * gunPoint.transform.forward;
    //    //Vector3 finalDirection = finalSpread * Camera.main.transform.forward;

    //    //Ray ray = new Ray(gunPoint.transform.position, finalDirection);
    //    Ray ray = Camera.main.ScreenPointToRay(crosshairScreenPos);
    //    RaycastHit hit;

    //    Debug.DrawRay(gunPoint.transform.position,ray.direction * weaponData.attackDistance, Color.red, 2f);

    //    if (Physics.Raycast(ray, out hit, weaponData.attackDistance))
    //    {
    //        GameObject possibleEnemy = hit.collider.gameObject;

    //        if (possibleEnemy.CompareTag("Enemy"))
    //        {
    //            possibleEnemy.GetComponent<EnemyBasic>().TakeDamage(weaponData.damage);
    //        }
    //    }

    //    cartridges--;
    //}
    //public void Fire()
    //{
    //    if (weaponData == null)
    //        return;

    //    // 1. Вызываем спрей оружия (чтобы рос счетчик shotCount патронов в зажиме)
    //    weaponRecoil.GetSpray();

    //    // 2. Считаем случайный угол разброса от движения (0 - idle, 1 - walk, 2 - jump)
    //    // Метод CalculateSpread возвращает Quaternion. Нам нужен радиус этого разброса.
    //    // Мы можем получить текущее значение разброса в градусах напрямую из твоего скрипта Spread
    //    float currentMovementSpread = spread.GetCurrentSpreadValue(movement.MovementType);

    //    // 3. Берем позицию прицела на экране
    //    Vector3 crosshairScreenPos = RectTransformUtility.WorldToScreenPoint(null, crosshairImage.rectTransform.position);

    //    // 4. Переводим градусы разброса в пиксели смещения на экране
    //    // Коэффициент (например, 15f) настраивает, насколько сильно пули будут косить на экране при беге/прыжках
    //    float pixelSpreadRadius = currentMovementSpread * 100f;

    //    // Генерируем случайное смещение внутри круга (чтобы разброс был круглым, а не квадратным)
    //    Vector2 randomOffset = UnityEngine.Random.insideUnitCircle * pixelSpreadRadius;

    //    // Добавляем случайный шум к экранной позиции выстрела
    //    crosshairScreenPos.x += randomOffset.x;
    //    crosshairScreenPos.y += randomOffset.y;

    //    // 5. Пускаем Raycast через итоговую точку с учетом рандома
    //    Ray ray = Camera.main.ScreenPointToRay(crosshairScreenPos);
    //    RaycastHit hit;

    //    // Отрисовка луча (длинная и точная)
    //    Debug.DrawRay(gunPoint.transform.position, ray.direction * weaponData.attackDistance, Color.red, 2f);

    //    if (Physics.Raycast(ray, out hit, weaponData.attackDistance))
    //    {
    //        GameObject possibleEnemy = hit.collider.gameObject;

    //        if (possibleEnemy.CompareTag("Enemy"))
    //        {
    //            possibleEnemy.GetComponent<EnemyBasic>().TakeDamage(weaponData.damage);
    //        }
    //    }

    //    cartridges--;
    //}
    public void Fire()
    {
        if (weaponData == null)
            return;

        //Quaternion spreadRotation = spread.CalculateSpread(movement.MovementType);

        //weaponRecoil.GetSpray();

        //Vector3 crosshairScreenPos = RectTransformUtility.WorldToScreenPoint(null, crosshairImage.rectTransform.position);

        //Ray ray = Camera.main.ScreenPointToRay(crosshairScreenPos);

        //Vector3 finalDirection = spreadRotation * ray.direction;

        //ray = new Ray(gunPoint.transform.position, finalDirection);

        //Quaternion spreadRot = spread.CalculateSpread(movement.MovementType); // тоже привести к (pitch=y, yaw=x)!
        //Vector2 sprayAngles = weaponRecoil.GetSpray();
        //Quaternion recoilRot = Quaternion.Euler(sprayAngles.y, sprayAngles.x, 0);

        //Vector3 baseDirection = Camera.main.transform.forward;
        //Vector3 finalDirection = recoilRot * spreadRot * baseDirection;

        //Ray ray = new Ray(gunPoint.transform.position, finalDirection);
        Quaternion spreadRot = spread.CalculateSpread(movement.MovementType); // тоже локальный оффсет, Euler(y, x, 0)
        Vector2 sprayAngles = weaponRecoil.GetSpray();
        Quaternion recoilRot = Quaternion.Euler(-sprayAngles.y, sprayAngles.x, 0);

        Vector3 finalDirection = Camera.main.transform.rotation * recoilRot * spreadRot * Vector3.forward;
        Ray ray = new Ray(gunPoint.transform.position, finalDirection);

        RaycastHit hit;

        Debug.DrawRay(gunPoint.transform.position, ray.direction * weaponData.attackDistance, Color.red, 20f);

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

    //public void Fire()
    //{
    //    if (weaponData == null) return;

    //    // 1. Получаем позицию картинки прицела на экране в пикселях.
    //    // Скрипт прицела двигает RectTransform, а эта строчка находит его точные координаты на экране:
    //    Vector3 crosshairScreenPos = RectTransformUtility.WorldToScreenPoint(null, crosshairImage.rectTransform.position);

    //    // 2. Пускаем Raycast из КАМЕРЫ, но СТРОГО через точку экрана, где сейчас находится прицел
    //    Ray ray = Camera.main.ScreenPointToRay(crosshairScreenPos);
    //    RaycastHit hit;

    //    // Отрисовка луча для проверки (теперь он будет длинным и точным)
    //    Debug.DrawRay(ray.origin, ray.direction * weaponData.attackDistance, Color.red, 2f);

    //    if (Physics.Raycast(ray, out hit, weaponData.attackDistance))
    //    {
    //        GameObject possibleEnemy = hit.collider.gameObject;
    //        if (possibleEnemy.CompareTag("Enemy"))
    //        {
    //            possibleEnemy.GetComponent<EnemyBasic>().TakeDamage(weaponData.damage);
    //        }
    //    }

    //    cartridges--;
    //}
    private void UpdateAmmo()
    {
        ammoText.text = $"{cartridges}";
        float fillAmount = (float)cartridges / weaponData.cartridges;
        AmmoCircle.fillAmount = fillAmount;
        if (cartridges < (float)weaponData.cartridges * 0.2f)
        {
            AmmoCircleMask.DOColor(Color.red, 0.5f);
        }
        else
        {
            Color yellowColor = new Color32(255, 219, 97, 255);
            AmmoCircleMask.DOColor(yellowColor, 0.5f);
        }
    }
    private IEnumerator Reloading()
    {
        //Debug.Log("Перезарядка");
        StartCoroutine(DelayBeforeQuitting(true));
        isReloading = true;
        animator.SetBool("reload", true);
        //canShoot = false;
        yield return new WaitForSeconds(weaponData.reloadSpeed);
        //Debug.Log("Перезарядка закончена");
        animator.SetBool("reload", false);
        //this.cartridges = weaponData.cartridges;
        //canShoot = true;
        isReloading = false;

    }
    private IEnumerator ReloadAmmoTextAnim()
    {
        int ammoToAdd = weaponData.cartridges - cartridges;
        float timeForOne = weaponData.reloadSpeed / ammoToAdd;
        for (int i = cartridges; i < weaponData.cartridges; i++)
        {
            Debug.Log($"Hi: {ammoToAdd}");
            yield return new WaitForSeconds(timeForOne);
            cartridges += 1;
        }
    }
    private IEnumerator AttackAnimDelay()
    {
        yield return new WaitForSeconds(weaponData.attackSpeed);
        animator.SetBool("isShooting", false);
    }
    //private IEnumerator AttackDelay()
    //{
    //  //  Debug.Log($"started coroutine AttackDelay");
    //    Debug.Log($"wait time: {weaponData.attackSpeed}");
    //    Debug.Log("waiting started");
    //    canCount = true;
    //    yield return new WaitForSeconds(weaponData.attackSpeed);
    //    canCount = false;
    //    animator.SetBool("isShooting", false);
    //  //  Debug.Log($"animator.GetBool(isShooting): {animator.GetBool("isShooting")}");

    //    canShoot = true;
    //}
    //private void Waiting()
    //{
    //    newTime += Time.deltaTime;
    //    //Debug.Log($"time waited: {newTime}");
    //}

    private IEnumerator DelayBeforeQuitting(bool waitLess)
    {
        if (waitLess)
        {
            yield return new WaitForSeconds(shotCooldownTimer - shotCooldownTimer * 0.01f - 0.05f);
        }
        else
        {
            yield return new WaitForSeconds(shotCooldownTimer - shotCooldownTimer * 0.01f);
        }

        animator.SetBool("isShooting", false);
        shotCooldownTimer = 0f;
    }
    public void EquipWeapon(WeaponData weaponData)
    {
        if (this.weaponData != null)
        {
            switch (this.weaponData.idName)
            {
                case "Pistol":
                    pistolCartriges = this.cartridges;
                    break;

                case "AK":
                    akCartriges = this.cartridges;
                    break;

                case "Laser":
                    laserCartriges = this.cartridges;
                    break;
            }
        }
        hasGun = true;
        this.weaponData = weaponData;
        switch (weaponData.idName)
        {
            case "Pistol":
                this.cartridges = weaponData.cartridges;
                if (pistolCartriges >= 0)
                {
                    this.cartridges = pistolCartriges;
                }
                break;

            case "AK":
                this.cartridges = weaponData.cartridges;
                if (akCartriges >= 0)
                {
                    this.cartridges = akCartriges;
                }
                break;

            case "Laser":
                this.cartridges = weaponData.cartridges;
                if (laserCartriges >= 0)
                {
                    this.cartridges = laserCartriges;
                }
                break;
        }
        foreach (var i in types)
        {
            if (i != this.weaponData.type)
            {
                animator.SetBool($"have{i}", false);
                animator.SetBool($"in{weaponData.type}", false);
            }
        }
        foreach (var i in PlayerWeapons)
        {
            if (i.gameObject.name == weaponData.idName)
            {
                usingWeapon = i.gameObject;
                i.gameObject.SetActive(true);
                animator.SetBool($"have{weaponData.type}", true);
                inSomething = true;
            }
            else
            {
                i.gameObject.SetActive(false);
            }
        }
    }
}