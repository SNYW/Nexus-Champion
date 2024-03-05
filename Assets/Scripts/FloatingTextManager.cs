using ObjectPooling;
using SystemEvents;
using UnityEngine;

public static class FloatingTextManager
{
  private static ObjectPool _textPool;
  public static void Init(ObjectPool textPool)
  {
    _textPool = textPool;
    SystemEventManager.Subscribe(SystemEventManager.SystemEventType.EnemyDamaged, OnEnemyDamaged);
  }

  private static void OnEnemyDamaged(object obj)
  {
    if (obj is not EnemyUnit.EnemyDamageEvent dmgEvent) return;

    if(dmgEvent.damageAmount > 0 && dmgEvent.unit.gameObject.activeSelf)
      SpawnFloatingDamageText(dmgEvent);
  }

  private static void SpawnFloatingDamageText(EnemyUnit.EnemyDamageEvent damageEvent)
  {
    if(!_textPool.GetPooledObject().TryGetComponent<FloatingText>(out var text)) return;
    
    text.gameObject.SetActive(true);
    text.Init(damageEvent);
  }
}
