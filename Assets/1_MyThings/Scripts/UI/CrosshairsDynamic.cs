using UnityEngine;
using UnityEngine.UI;

public class CrosshairDynamic : MonoBehaviour
{
    [Header("Ссылки")]
    [SerializeField] private Movement1 movement;
    [SerializeField] private RectTransform crosshairRect;

    [Header("Настройки формы")]
    [SerializeField] private Vector2 normalSize = new Vector2(1f, 1f);
    [SerializeField] private Vector2 dynamicMoveSize = new Vector2(3f, 3f);
    [SerializeField] private Vector2 dynamicJumpSize = new Vector2(4f, 4f);
    [SerializeField] private float transitionSpeed = 10f;



    void Update()
    {
        Vector2 targetSize;

        targetSize = normalSize;
        if (movement.MovementType == 1)
        {
            targetSize = dynamicMoveSize;
        }
        else if (movement.MovementType == 2)
        {
            targetSize = dynamicJumpSize;
        }

        crosshairRect.localScale = Vector3.Lerp(crosshairRect.localScale, new Vector3(targetSize.x, targetSize.y, 1f), Time.deltaTime * transitionSpeed);
    }

}
