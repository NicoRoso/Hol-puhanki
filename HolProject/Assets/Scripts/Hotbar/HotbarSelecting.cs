using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HotbarSelecting : MonoBehaviour
{
    [SerializeField] SelectSlot _currentSlot;
    public List<SelectSlot> _slots = new List<SelectSlot>();
    
    private void Awake()
    {
        foreach (var slot in GetComponentsInChildren<SelectSlot>())
        {
            slot.OnSlotDeselect();
            _slots.Add(slot);
        }
        _currentSlot = _slots[0];
    }

    public void SelectSlotByKey(int id)
    {
        _currentSlot.OnSlotDeselect();
        _currentSlot = _slots[id];
        _currentSlot.OnSlotSelect();
    }
}
