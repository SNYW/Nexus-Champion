using UnityEngine;
using UnityEngine.UI;

public class UnitHealthbar : MonoBehaviour
{
    public Image healthIndicator;
    public Image healthIndicatorSecondary;
    public float lerpSpeed;

    public void UpdateHealthIndicator(int currentHealth, int maxHealth)
    {
        var targetValue = (float)currentHealth / maxHealth;
        
        LeanTween.value(healthIndicator.fillAmount, targetValue, lerpSpeed)
            .setOnUpdate(val => healthIndicator.fillAmount = val)
            .setEaseLinear();
        
        LeanTween.value(healthIndicatorSecondary.fillAmount, targetValue, lerpSpeed)
            .setOnUpdate(val => healthIndicatorSecondary.fillAmount = val)
            .setEaseLinear()
            .setDelay(lerpSpeed);
    }

    public void Reset()
    {
        healthIndicator.fillAmount = 1;
        healthIndicatorSecondary.fillAmount = 1;
    }
}
