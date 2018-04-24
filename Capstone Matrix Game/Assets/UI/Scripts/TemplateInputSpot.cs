using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TemplateInputSpot : MonoBehaviour, IDropHandler
{	
	private MatrixInputManager matrixInputManager;

	public bool AcceptingInput
	{
		get
		{
			return acceptingInput;
		}
		set
		{
			acceptingInput = value;
			GetComponent<Image>().color = acceptingInput ? acceptingInputColor : notAcceptingInputColor;
			//Debug.Log("Input slot now " + (!acceptingInput ? "not" : "") + " accepting input.");
		}
	}
	private bool acceptingInput;
    public Color acceptingInputColor;
	public Color notAcceptingInputColor;

	public GameObject storedTemplateObject
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
        if (acceptingInput && storedTemplateObject == null && DragHandler.template != null)
        {
			//find the template that is currently being dragged
			MatrixInputTemplate draggedTemplate = DragHandler.template.GetComponent<MatrixInputTemplate>();

            draggedTemplate.inWorkspace = true;
			draggedTemplate.SetAcceptingInput(true);

			DragHandler.template.transform.SetParent(transform);

			matrixInputManager.WorkspaceChanged();
		}
    }
}
