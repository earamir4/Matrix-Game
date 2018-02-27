using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public static GameObject template;
    Vector3 startPosition;
    Transform startParent;

    //DragHandler
    public void OnBeginDrag(PointerEventData eventData)
    {
        template = gameObject;
        startPosition = transform.position;
        startParent = transform.parent;
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<Canvas>().overrideSorting = true;
        GetComponent<Canvas>().sortingOrder = 1;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        template = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        GetComponent<Canvas>().overrideSorting = false;
        transform.localPosition = Vector3.zero;
    }
}
