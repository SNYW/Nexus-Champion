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
    
    if(dmgEvent.damageAmount > 0)
      SpawnFloatingDamageText(dmgEvent);
  }

  private static void SpawnFloatingDamageText(EnemyUnit.EnemyDamageEvent damageEvent)
  {
    if(!_textPool.GetPooledObject().TryGetComponent<FloatingText>(out var text)) return;

    text.transform.position = damageEvent.unit.transform.position + Vector3.up * 2;
    text.Init(damageEvent.damageAmount);
    text.gameObject.SetActive(true);
  }
}
