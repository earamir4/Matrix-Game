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
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        template = null;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        transform.localPosition = Vector3.zero;
    }
}
