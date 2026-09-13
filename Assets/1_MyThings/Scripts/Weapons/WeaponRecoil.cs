using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] private float recoilTimeout = 0.133f;

    [Header("Spray")]
    [SerializeField]
    private Vector2[] sprayPattern = new Vector2[]
    {
        new Vector2(0f, 0f),
        new Vector2(0f, 6f),
        new Vector2(-1f, 7f),
        new Vector2(1f, 7.5f),
        new Vector2(2f, 8f)
    };

    //private int shotCount = 0;
    //private float recoilTimer = 0.15f;
    private int shotCount = 0;
    private float lastShotTime = 0;

    //private void Update()
    //{
    //    if (recoilTimer > 0)
    //    {
    //        recoilTimer -= Time.deltaTime;
    //        if (recoilTimer <= 0)
    //        {
    //            shotCount = 0;
    //        }
    //    }
    //}

    //public Quaternion GetSpray()
    //{
    //    recoilTimer = recoilTimeout;

    //    float xSpread = 0f;
    //    float ySpread = 0f;

    //    if (sprayPattern.Length > 0)
    //    {
    //        // Ѕерем индекс текущей пули (если зажали дольше массива Ч зацикливаем последние точки)
    //        int index = Mathf.Min(shotCount, sprayPattern.Length - 1);

    //        // ‘иксированный увод из рисунка + капелька случайности, чтобы не было лазера
    //        xSpread = sprayPattern[index].x + Random.Range(-0.1f, 0.1f);
    //        ySpread = sprayPattern[index].y + Random.Range(-0.1f, 0.1f);
    //    }

    //    shotCount++;

    //    Debug.Log($"ѕатрон є{shotCount}. ”вод пули в градусах: X = {xSpread}, Y = {ySpread}");

    //    //return Quaternion.Euler(ySpread, xSpread, 0);
    //    //return Quaternion.Euler(-ySpread, xSpread, 0);
    //    return Quaternion.Euler(ySpread, xSpread, 0);
    //}
    //public Vector2 GetCurrentSprayAngles()
    //{
    //    if (sprayPattern == null || sprayPattern.Length == 0) return Vector2.zero;

    //    // Ѕерем индекс текущей пули, как и при выстреле
    //    int index = Mathf.Min(shotCount, sprayPattern.Length - 1);
    //    return sprayPattern[index];
    //}
    //public void GetSpray()
    //{
    //    recoilTimer = recoilTimeout;

    //    float xSpread = 0f;
    //    float ySpread = 0f;

    //    if (sprayPattern.Length > 0)
    //    {
    //        // Ѕерем индекс текущей пули (если зажали дольше массива Ч зацикливаем последние точки)
    //        int index = Mathf.Min(shotCount, sprayPattern.Length - 1);

    //        // ‘иксированный увод из рисунка + капелька случайности, чтобы не было лазера
    //        xSpread = sprayPattern[index].x + Random.Range(-0.1f, 0.1f);
    //        ySpread = sprayPattern[index].y + Random.Range(-0.1f, 0.1f);
    //    }

    //    shotCount++;

    //    Debug.Log($"ѕатрон є{shotCount}. ”вод пули в градусах: X = {xSpread}, Y = {ySpread}");

    //    //return Quaternion.Euler(ySpread, xSpread, 0);
    //    //return Quaternion.Euler(-ySpread, xSpread, 0);
    //    //return Quaternion.Euler(ySpread, xSpread, 0);
    //}
    //public Vector2 GetSpray()
    //{
    //    recoilTimer = recoilTimeout;

    //    Vector2 result = Vector2.zero;
    //    if (sprayPattern.Length > 0)
    //    {
    //        int index = Mathf.Min(shotCount, sprayPattern.Length - 1);
    //        result = sprayPattern[index] + new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
    //    }

    //    shotCount++;
    //    return result; // x = горизонталь(yaw), y = вертикаль(pitch) Ч фиксируем это как единый стандарт
    //}
    private void Update()
    {
        // тикает каждый кадр Ќ≈«ј¬»—»ћќ от стрельбы Ч
        // сбрасывает паттерн, если давно не стрел€ли
        if (shotCount > 0 && Time.time - lastShotTime > recoilTimeout)
        {
            shotCount = 0;
        }
    }

    public Vector2 GetSpray()
    {
        // здесь сброс больше не нужен Ч этим теперь занимаетс€ Update()
        lastShotTime = Time.time;

        Vector2 result = Vector2.zero;
        if (sprayPattern.Length > 0)
        {
            int index = Mathf.Min(shotCount, sprayPattern.Length - 1);
            result = sprayPattern[index] + new Vector2(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f));
        }

        shotCount++;
        return result;
    }
    public Vector2 GetCurrentSprayAngles()
    {
        if (sprayPattern == null || sprayPattern.Length == 0) return Vector2.zero;

        // Ѕерем индекс текущей пули, как и при выстреле
        int index = Mathf.Min(shotCount, sprayPattern.Length - 1);
        return sprayPattern[index];
    }
}
