﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MatrixSlot : MonoBehaviour, IDropHandler
{
	public bool workspaceSlot;
    private MatrixInputManager matrixInputManager;

	public GameObject item
	{
		get
		{
			if (transform.childCount > 0)
				return transform.GetChild(0).gameObject;

			return null;
		}
	}

	private void Start()
    {
		if (matrixInputManager == null)
		{
			matrixInputManager = GameObject.FindObjectOfType<MatrixInputManager>();
		}
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (item == null)
        {
			//disable input on the template that was just dragged into this slot
			MatrixInputTemplate draggedTemplate = DragHandler.template.GetComponent<MatrixInputTemplate>();

			draggedTemplate.ToggleInput();

			if (workspaceSlot)
			{
                matrixInputManager.AddWorkingTemplate(draggedTemplate);
			}

			DragHandler.template.transform.SetParent(transform);
        }
    }
}
