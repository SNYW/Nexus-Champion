using System;
using SystemEvents;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
   public GameObject screen;
   private void Start()
   { 
       screen.gameObject.SetActive(false); 
       SystemEventManager.Subscribe(SystemEventManager.SystemEventType.GameEnd, EnablePanel);
       SystemEventManager.Subscribe(SystemEventManager.SystemEventType.GameStart, DisablePanel);
   }

   private void DisablePanel(object obj)
   {
     screen.gameObject.SetActive(false);
   }

   private void EnablePanel(object obj)
   {
       screen.gameObject.SetActive(true);
   }

   private void OnDisable()
   {
       SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.GameEnd, EnablePanel);
       SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.GameStart, DisablePanel);
   }
}
