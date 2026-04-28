using UnityEngine;
using UnityEngine.UI;

public class HealAbility : Ability
{
    public Image currentHealthGlobe;
    public float hitPoint = 20f;
    public float maxHitPoint = 100f;

    private void Awake()
    {
        UpdateHealthGlobe();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Activate();
        }
    }
    public override void Activate()
    {
        HealDamage(20f);
    }
    public void HealDamage(float Heal)
    {
        hitPoint += Heal;
        if (hitPoint > maxHitPoint)
            hitPoint = maxHitPoint;

        UpdateHealthGlobe();
    }
    private void UpdateHealthGlobe()
    {
        float ratio = hitPoint / maxHitPoint;
        currentHealthGlobe.rectTransform.localPosition = new Vector3(0, currentHealthGlobe.rectTransform.rect.height * ratio - currentHealthGlobe.rectTransform.rect.height, 0);
    }
}
