using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSystemBinder : MonoBehaviour
{
    public SpellSystem boundSystem;
    
    public void CastSpellFromAnimation(int index)
    {
        boundSystem.CastSpellFromAnimation(index);
    }
}
