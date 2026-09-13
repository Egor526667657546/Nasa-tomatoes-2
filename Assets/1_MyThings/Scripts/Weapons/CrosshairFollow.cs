using UnityEngine;

public class CrosshairFollow : MonoBehaviour
{
    [SerializeField] private WeaponRecoil weaponRecoil;
    [SerializeField] private Camera playerCamera;
    //[SerializeField] private float shiftMultiplier = 20f; // Чувствительность: сколько пикселей экрана давать за 1 градус спрея
    [SerializeField] private float smoothSpeed = 15f;     // Скорость плавного возврата/движения прицела

    private RectTransform rectTransform;
    private Vector2 centerPosition;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        centerPosition = rectTransform.anchoredPosition;
    }

    //private void Update()
    //{
    //    if (weaponRecoil == null) return;

    //    Vector2 sprayAngles = weaponRecoil.GetCurrentSprayAngles();

    //    float targetX = centerPosition.x + (sprayAngles.x * shiftMultiplier);
    //    float targetY = centerPosition.y + (sprayAngles.y * shiftMultiplier);

    //    Vector2 targetPosition = new Vector2(targetX, targetY);

    //    rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, Time.deltaTime * smoothSpeed);
    //}
    private void Update()
    {
        if (weaponRecoil == null || playerCamera == null) return;

        Vector2 sprayAngles = weaponRecoil.GetCurrentSprayAngles();

        // 1. Строим вектор направления пули в 3D (точно так же, как в скрипте стрельбы)
        Quaternion sprayRot = Quaternion.Euler(sprayAngles.y, sprayAngles.x, 0);
        Vector3 targetDirection = sprayRot * playerCamera.transform.forward;

        // 2. Находим виртуальную точку в мире на расстоянии, например, 10 метров перед камерой
        Vector3 worldPoint = playerCamera.transform.position + (targetDirection * 10f);

        // 3. Переводим эту 3D точку в пиксели на экране
        Vector3 screenPoint = playerCamera.WorldToScreenPoint(worldPoint);

        // 4. Корректируем позицию под Canvas (вычитаем половину экрана, так как у UI центр в 0,0)
        Vector2 targetPos = new Vector2(screenPoint.x - (Screen.width / 2f), screenPoint.y - (Screen.height / 2f));

        // 5. Плавно двигаем прицел в эту точку
        rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPos, Time.deltaTime * smoothSpeed);
    }
}
