using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TemplateStorageSlot : MonoBehaviour, IDropHandler
{
	private MatrixInputManager matrixInputManager;

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
		if (storedTemplateObject == null && DragHandler.template != null)
		{
			//disable input on the template that was just dragged into this slot
			MatrixInputTemplate draggedTemplate = DragHandler.template.GetComponent<MatrixInputTemplate>();

			draggedTemplate.SetAcceptingInput(false);

			DragHandler.template.transform.SetParent(transform);

			matrixInputManager.WorkspaceChanged();
		}
	}
}
