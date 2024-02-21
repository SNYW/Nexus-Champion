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
    public Image[] cooldownIndicators;
    public Image overlayedCooldownIndiator;
    public bool selected;
    public bool isOnCooldown;

    private void Start()
    {
        selected = false;
        isOnCooldown = false;
        foreach (var indicator in cooldownIndicators)
        {
            indicator.fillAmount = 1;
        }
    }

    void Update()
    {
        if(spell == null) return;
        
        if (isOnCooldown) return;
        
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
            isOnCooldown = true;
            ManageCooldown();
        }
    }

    private void ManageCooldown()
    {
        overlayedCooldownIndiator.enabled = true;
        foreach (var indicator in cooldownIndicators)
        {
            LeanTween.value(gameObject, 0, 1, spell.coolDown)
                .setOnUpdate(val => indicator.fillAmount = val);
        }

        LeanTween.value(gameObject, 0, 1, spell.coolDown).setOnComplete(() =>
        {
            isOnCooldown = false;
            overlayedCooldownIndiator.enabled = false;
        });
    }
}
