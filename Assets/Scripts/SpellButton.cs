using System;
using System.Collections;
using System.Collections.Generic;
using Spells;
using SystemEvents;
using UnityEngine;

public class SpellButton : MonoBehaviour
{
    public KeyCode hotkey;
    public Spell spell;

    public bool selected;

    private void Start()
    {
        selected = false;
    }

    void Update()
    {
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
        }
    }
}
