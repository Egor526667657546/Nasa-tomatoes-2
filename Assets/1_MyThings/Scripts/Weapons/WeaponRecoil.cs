using UnityEngine;

public class WeaponRecoil : MonoBehaviour
{
    [SerializeField] private float recoilTimeout = 0.15f;

    [Header("Spray")]
    [SerializeField]
    private Vector2[] sprayPattern = new Vector2[]
    {
        new Vector2(0f, 0f),   // 1-€ пул€ чуть выше
        new Vector2(0f, 6f),   // 2-€ еще выше
        new Vector2(-1f, 7f),  // 3-€ уходит влево и вверх
        new Vector2(1f, 7.5f), // 4-€ уходит вправо
        new Vector2(2f, 8f)    // 5-€ еще правее
    };

    private int shotCount = 0;
    private float recoilTimer = 0f;

    private void Update()
    {
        if (recoilTimer > 0)
        {
            recoilTimer -= Time.deltaTime;
            if (recoilTimer <= 0)
            {
                shotCount = 0;
            }
        }
    }

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
    public void GetSpray()
    {
        recoilTimer = recoilTimeout;

        float xSpread = 0f;
        float ySpread = 0f;

        if (sprayPattern.Length > 0)
        {
            // Ѕерем индекс текущей пули (если зажали дольше массива Ч зацикливаем последние точки)
            int index = Mathf.Min(shotCount, sprayPattern.Length - 1);

            // ‘иксированный увод из рисунка + капелька случайности, чтобы не было лазера
            xSpread = sprayPattern[index].x + Random.Range(-0.1f, 0.1f);
            ySpread = sprayPattern[index].y + Random.Range(-0.1f, 0.1f);
        }

        shotCount++;

        Debug.Log($"ѕатрон є{shotCount}. ”вод пули в градусах: X = {xSpread}, Y = {ySpread}");

        //return Quaternion.Euler(ySpread, xSpread, 0);
        //return Quaternion.Euler(-ySpread, xSpread, 0);
        //return Quaternion.Euler(ySpread, xSpread, 0);
    }
    public Vector2 GetCurrentSprayAngles()
    {
        if (sprayPattern == null || sprayPattern.Length == 0) return Vector2.zero;

        // Ѕерем индекс текущей пули, как и при выстреле
        int index = Mathf.Min(shotCount, sprayPattern.Length - 1);
        return sprayPattern[index];
    }
}
