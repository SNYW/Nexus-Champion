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

   public void Init(int dmgAmount)
   {
      text.text = dmgAmount.ToString();
      _startCanvasPos = canvas.transform.position;

      LeanTween.moveLocalY(canvas.gameObject, canvas.transform.position.y + moveAmount, 0.3f);
      LeanTween.alphaCanvas(canvasGroup, 0, 1.2f).setOnComplete(ReQueue);
      LeanTween.scale(gameObject, new Vector3(2, 2, 2), 0.8f).setEasePunch();
   }

   private void OnDisable()
   {
      canvas.transform.position = _startCanvasPos;
      canvasGroup.alpha = 1;
      transform.localScale = Vector3.one;
      transform.localPosition = Vector3.zero;
      transform.rotation = Quaternion.identity;
   }
}
