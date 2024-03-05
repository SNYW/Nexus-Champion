using System;
using ObjectPooling;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class FloatingText : PooledObject
{
   public float moveAmount;
   public TMP_Text text;
   public Canvas canvas;
   public CanvasGroup canvasGroup;

   private Vector3 _startCanvasPos;

   private void OnEnable()
   {
      transform.rotation = Quaternion.identity;
      canvas.transform.localPosition = Vector3.zero;
   }

   public void Init(EnemyUnit.EnemyDamageEvent dmgEvent)
   {
      text.text = dmgEvent.damageAmount.ToString();
      transform.position = dmgEvent.unit.transform.position;
      canvasGroup.alpha = 1;
      
      _startCanvasPos = canvas.transform.position;

      LeanTween.moveY(canvas.gameObject, transform.position.y + moveAmount, 0.3f);
      LeanTween.alphaCanvas(canvasGroup, 0, 1.2f).setOnComplete(ReQueue);
      LeanTween.scale(gameObject, new Vector3(2, 2, 2), 0.8f).setEasePunch();
   }

   private void OnDisable()
   {
      LeanTween.cancelAll(gameObject);
      canvas.transform.position = _startCanvasPos;
      canvasGroup.alpha = 1;
      transform.localScale = Vector3.one;
   }
}
