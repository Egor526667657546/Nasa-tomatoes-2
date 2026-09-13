using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HpSlider : MonoBehaviour
{
    [SerializeField] private PlayerHealthSystem health;
    [SerializeField] private Slider slider;
    [SerializeField] private Image healthbarImage;

    [SerializeField] private TextMeshProUGUI hpText;

    private readonly Color yellowColor = new Color32(255, 219, 97, 255);
    private readonly Color redColor = Color.red;

    private void Update()
    {
        slider.value = health.Health;
        hpText.text = $"{health.Health}";

        float hpPercent = (float)health.Health / health.MaxHp;

        if (hpPercent >= 0.15f)
        {
            healthbarImage.color = yellowColor;
        }
        else if (hpPercent > 0.10f)
        {
            float transition = (0.15f - hpPercent) / 0.05f;

            healthbarImage.color = Color.Lerp( yellowColor, redColor, transition);
        }
        else
        {
            healthbarImage.color = redColor;
        }
    }
}