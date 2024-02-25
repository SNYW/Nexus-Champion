using SystemEvents;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthIndicator : MonoBehaviour
{
    public Image healthIndicator;
    public Image healthIndicatorSecondary;
    public float lerpSpeed;
    private void OnEnable()
    {
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.PlayerDamaged, OnPlayerDamaged);
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.GameEnd, OnGameEnd);
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.GameStart, OnGameStart);
    }
    
    private void OnGameStart(object obj)
    {
        healthIndicator.fillAmount = 1;
        healthIndicatorSecondary.fillAmount = 1;
    }

    private void OnGameEnd(object obj)
    {
        healthIndicator.fillAmount = 0;
        healthIndicatorSecondary.fillAmount = 0;
        LeanTween.cancel(gameObject);
    }

    private void OnPlayerDamaged(object obj)
    {
        if (obj is not PlayerUnit unit) return;

        var targetValue = (float)unit.currentHealth/unit.maxHealth;
        LeanTween.value(gameObject,healthIndicator.fillAmount, targetValue, lerpSpeed)
            .setOnUpdate(val => healthIndicator.fillAmount = val)
            .setEaseLinear();
        
        LeanTween.value(gameObject,healthIndicatorSecondary.fillAmount, targetValue, lerpSpeed)
            .setOnUpdate(val => healthIndicatorSecondary.fillAmount = val)
            .setEaseLinear()
            .setDelay(lerpSpeed);
    }

    private void OnDisable()
    {
       SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.PlayerDamaged, OnPlayerDamaged);
       SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.GameEnd, OnGameEnd);
       SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.GameStart, OnGameStart);
    }
}
