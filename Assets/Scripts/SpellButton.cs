using System;
using System.Collections;
using System.Collections.Generic;
using Spells;
using SystemEvents;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{
    public KeyCode hotkey;
    public Spell spell;
    public Image cooldownIndicator;
    public bool selected;

    private float currentCooldown;

    private void Start()
    {
        selected = false;
        currentCooldown = 0;
    }

    void Update()
    {
        if(spell != null)
            ManageCooldown();
        if (currentCooldown > 0) return;
        
        if (Input.GetKeyDown(hotkey))
        {
            selected = true;
            SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.SpellSelected, spell);
        }
        
        if (Input.GetMouseButton(1) && selected)
        {
            selected = false;
            SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.SpellDeselected, spell);
        }

        if ((Input.GetKeyUp(hotkey) || Input.GetMouseButtonDown(0)) && selected)
        {
            selected = false;
            SystemEventManager.RaiseEvent(SystemEventManager.SystemEventType.SpellCast, spell);
            currentCooldown = spell.coolDown;
        }
    }

    private void ManageCooldown()
    {
        currentCooldown = Mathf.Clamp(currentCooldown - Time.deltaTime, 0 , float.MaxValue);
        cooldownIndicator.fillAmount = currentCooldown / spell.coolDown;
    }
}
