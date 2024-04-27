using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectSlot : MonoBehaviour
{
    [SerializeField] Color _notSelected;
    [SerializeField] Color _selected;

    [SerializeField] Image _slotIcon;

    private void GetSlot()
    {
        _slotIcon= transform.GetChild(0).GetComponent<Image>();
    }
    public void OnSlotSelect()
    {
        GetComponent<Image>().color = _selected;
    }
    public void OnSlotDeselect()
    {
        GetComponent<Image>().color = _notSelected;
    }


    public void SetImage(Sprite icon)
    {
        if (_slotIcon is null) GetSlot();
        _slotIcon.enabled = true;
        _slotIcon.sprite = icon;
    }

    public void RemoveImage()
    {
        if (_slotIcon is null) GetSlot();
        _slotIcon.enabled = false;
    }
}
