using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public Image image;
    public string nameObj;
    public string desc;
    [HideInInspector] public Transform parentAfterDrag;
    public bool isSword;
    public void OnBeginDrag(PointerEventData eventData)
    {
        image= GetComponent<Image>();
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        image.raycastTarget = false;
        GameObject.FindGameObjectWithTag("InfoWindow").GetComponent<InfoWindow>().Fill(nameObj, desc, image.sprite);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDrag);
        image.raycastTarget = true;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject.FindGameObjectWithTag("InfoWindow").GetComponent<InfoWindow>().Fill(nameObj, desc, image.sprite);
    }

}
