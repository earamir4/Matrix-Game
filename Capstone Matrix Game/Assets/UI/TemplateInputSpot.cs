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
		set
		{
			acceptingInput = value;
			GetComponent<Image>().color = acceptingInput ? acceptingInputColor : notAcceptingInputColor;
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
		AcceptingInput = false;
		if (matrixInputManager == null)
		{
			matrixInputManager = GameObject.FindObjectOfType<MatrixInputManager>();
		}
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (acceptingInput && storedTemplateObject == null)
        {
			//disable input on the template that was just dragged into this slot
			MatrixInputTemplate draggedTemplate = DragHandler.template.GetComponent<MatrixInputTemplate>();

			draggedTemplate.SetAcceptingInput(true);

			matrixInputManager.SpotAcceptedInput();

			DragHandler.template.transform.SetParent(transform);
        }
    }
}
