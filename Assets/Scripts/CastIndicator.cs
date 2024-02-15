using Spells;
using SystemEvents;
using UnityEngine;

public class CastIndicator : MonoBehaviour
{
    private Camera _mainCamera;
    public GameObject indicatorParent;
    public LayerMask mask;

    private GameObject _indicatorObject;
    private void OnEnable()
    {
        _mainCamera = Camera.main;
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.SpellSelected, OnSpellSelect);
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.SpellCast, OnSpellDeselect);
        SystemEventManager.Subscribe(SystemEventManager.SystemEventType.SpellDeselected, OnSpellDeselect);
    }

    private void OnSpellSelect(object obj)
    {
        if (obj is not Spell spell) return;
        
        indicatorParent.SetActive(true);
        transform.forward = MouseManager.GetDirectionToMouse(transform.position, mask);
        _indicatorObject = Instantiate(spell.indicatorPrefab, indicatorParent.transform);
    }

    private void OnSpellDeselect(object obj)
    {
        Destroy(_indicatorObject.gameObject);
    }
    
    void Update()
    {
        if (_indicatorObject == null) return;

        transform.forward = MouseManager.GetDirectionToMouse(transform.position, mask);
    }

    private void OnDisable()
    {
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.SpellSelected, OnSpellSelect);
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.SpellDeselected, OnSpellDeselect);
        SystemEventManager.Unsubscribe(SystemEventManager.SystemEventType.SpellCast, OnSpellDeselect);
    }
}
