using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    [SerializeField] Image _slotIcon;
    public void OnDrop(PointerEventData eventData)
    {
        
        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            if (!draggableItem.isSword) draggableItem.parentAfterDrag = transform;
        }
        else
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

            if (!draggableItem.isSword)
            {
                GameObject current = transform.GetChild(0).gameObject;
                DraggableItem currentDraggable = current.GetComponent<DraggableItem>();

                currentDraggable.transform.SetParent(draggableItem.parentAfterDrag);
                draggableItem.parentAfterDrag = transform;
            }
        }
    }

    private void GetSlot()
    {
        _slotIcon = transform.GetChild(0).GetComponent<Image>();
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
