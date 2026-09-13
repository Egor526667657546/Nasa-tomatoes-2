using UnityEngine;

public class Spread : MonoBehaviour
{
    [Header("Idle")]
    [SerializeField] private float baseIdleSpread = 0f;
    [SerializeField] private float maxIdleSpread = 10f;
    [SerializeField] private float idleSpreadPerShot = 1.5f;

    [Header("Move")]
    [SerializeField] private float baseMoveSpread = 4f;
    [SerializeField] private float maxMoveSpread = 16f;
    [SerializeField] private float moveSpreadPerShot = 2.5f;

    [Header("Jump")]
    [SerializeField] private float baseJumpSpread = 12f;
    [SerializeField] private float maxJumpSpread = 35f;
    [SerializeField] private float jumpSpreadPerShot = 8f;

    [Header("Restore")]
    [SerializeField] private float restoreSpeed = 8f;
    [SerializeField] private float restoreDelay = 0.15f;


    private float currentIdleSpread;
    private float currentMoveSpread;
    private float currentJumpSpread;

    private float restoreTimer = 0f;

    private void Start()
    {
        currentIdleSpread = baseIdleSpread;
        currentMoveSpread = baseMoveSpread;
        currentJumpSpread = baseJumpSpread;
    }

    private void Update()
    {
        currentMoveSpread = Mathf.Max(currentMoveSpread, currentIdleSpread);
        currentJumpSpread = Mathf.Max(currentJumpSpread, currentMoveSpread);

        if (restoreTimer > 0)
        {
            restoreTimer -= Time.deltaTime;
        }

        if (restoreTimer <= 0)
        {
            currentIdleSpread = Mathf.Lerp(currentIdleSpread, baseIdleSpread, Time.deltaTime * restoreSpeed);
            currentMoveSpread = Mathf.Lerp(currentMoveSpread, baseMoveSpread, Time.deltaTime * restoreSpeed);
            currentJumpSpread = Mathf.Lerp(currentJumpSpread, baseJumpSpread, Time.deltaTime * restoreSpeed * 0.5f);
        }
    }

    public Quaternion CalculateSpread(int movementType) // 0 - idle, 1 - walk, 2 - jump
    {
        restoreTimer = restoreDelay;
        float xSpread = 0f;
        float ySpread = 0f;
        Quaternion spreadRotation = Quaternion.identity;

        switch (movementType)
        {
            case 0:
                xSpread = Random.Range(-currentIdleSpread, currentIdleSpread);
                ySpread = Random.Range(-currentIdleSpread, currentIdleSpread);

                currentIdleSpread = Mathf.Min(currentIdleSpread + idleSpreadPerShot, maxIdleSpread);
                break;

            case 1:
                xSpread = Random.Range(-currentMoveSpread, currentMoveSpread);
                ySpread = Random.Range(-currentMoveSpread, currentMoveSpread);

                currentMoveSpread = Mathf.Min(currentMoveSpread + moveSpreadPerShot, maxMoveSpread);
                break;

            case 2:
                xSpread = Random.Range(-currentJumpSpread, currentJumpSpread);
                ySpread = Random.Range(-currentJumpSpread, currentJumpSpread);

                currentJumpSpread = Mathf.Min(currentJumpSpread + jumpSpreadPerShot, maxJumpSpread);
                break;

            default:
                Debug.Log("bug");
                break;
        }
        spreadRotation = Quaternion.Euler(ySpread, xSpread, 0);
        //Debug.Log($"Текущий режим: {movementType}. Углы разброса в градусах: X = {xSpread}, Y = {ySpread}");
        return spreadRotation;
    }
    public float GetCurrentSpreadValue(int movementType)
    {
        switch (movementType)
        {
            case 0: return currentIdleSpread;
            case 1: return currentMoveSpread;
            case 2: return currentJumpSpread;
            default: return 0f;
        }
    }

}