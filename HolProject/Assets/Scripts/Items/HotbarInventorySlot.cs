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
            if (!draggableItem.isSword) draggableItem.parentAfterDrag = transform;
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

}
