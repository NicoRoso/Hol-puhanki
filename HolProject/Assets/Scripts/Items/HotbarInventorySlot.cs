using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HotbarInventorySlot : MonoBehaviour, IDropHandler
{
    public Inventory inventory;
    public Image _slotIcon;
    public int id;
    public void OnDrop(PointerEventData eventData)
    {

        if (transform.childCount == 0)
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
            try
            {
                inventory.Change(draggableItem.parentAfterDrag.gameObject.GetComponent<HotbarInventorySlot>().id, id);
            }
            catch
            {
                inventory.CrossChange(draggableItem.parentAfterDrag.gameObject.GetComponent<InventorySlot>().id, id);
            }
             draggableItem.parentAfterDrag = transform;
        }
        else
        {
            GameObject dropped = eventData.pointerDrag;
            DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();


                GameObject current = transform.GetChild(0).gameObject;
                DraggableItem currentDraggable = current.GetComponent<DraggableItem>();

                currentDraggable.transform.SetParent(draggableItem.parentAfterDrag);
                try
                {
                    inventory.Swap(draggableItem.parentAfterDrag.gameObject.GetComponent<HotbarInventorySlot>().id, id);
                }
                catch
                {
                    inventory.CrossSwap(draggableItem.parentAfterDrag.gameObject.GetComponent<InventorySlot>().id, id);
                }
                draggableItem.parentAfterDrag = transform;

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
