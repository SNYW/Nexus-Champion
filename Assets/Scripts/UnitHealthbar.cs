using UnityEngine;
using UnityEngine.UI;

public class UnitHealthbar : MonoBehaviour
{
    public Image healthIndicator;
    public Image healthIndicatorSecondary;
    public GameObject canvas;
    public float lerpSpeed;

    private void OnEnable()
    {
        canvas.SetActive(false);
    }

    public void UpdateHealthIndicator(int currentHealth, int maxHealth)
    {
        canvas.SetActive(true);
        
        var targetValue = (float)currentHealth / maxHealth;
        
        LeanTween.value(gameObject,healthIndicator.fillAmount, targetValue, lerpSpeed)
            .setOnUpdate(val => healthIndicator.fillAmount = val)
            .setEaseLinear();
        
        LeanTween.value(gameObject,healthIndicatorSecondary.fillAmount, targetValue, lerpSpeed)
            .setOnUpdate(val => healthIndicatorSecondary.fillAmount = val)
            .setEaseLinear()
            .setDelay(lerpSpeed);
    }

    public void Reset()
    {
        LeanTween.cancel(gameObject);
        healthIndicator.fillAmount = 1;
        healthIndicatorSecondary.fillAmount = 1;
    }

}
