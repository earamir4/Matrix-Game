using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RenderAreaZoom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public Camera matrixRenderCamera;
	public float maxOrthoSize;
	public float minOrthoSize;
	public float baseOrthoSize;
	public float sizeChangeRate;
	private float orthoSize;
	private bool isPointerInside = false;

	public CartesianRender cartesianRenderer;

	public void Start()
	{
		matrixRenderCamera.orthographicSize = baseOrthoSize;
    }

	public void Update()
	{
		matrixRenderCamera.orthographicSize = orthoSize;

		if (!isPointerInside)
			return;

		if (Input.GetAxis("Mouse ScrollWheel") > 0)
		{
			orthoSize = Mathf.Clamp(orthoSize -= sizeChangeRate, minOrthoSize, maxOrthoSize);
			cartesianRenderer.toolTipRenderSize = orthoSize / baseOrthoSize;
        }
		else if (Input.GetAxis("Mouse ScrollWheel") < 0)
		{
			orthoSize = Mathf.Clamp(orthoSize += sizeChangeRate, minOrthoSize, maxOrthoSize);
			cartesianRenderer.toolTipRenderSize = orthoSize / baseOrthoSize;
		}
    }

	public void OnPointerEnter(PointerEventData eventData)
	{
		isPointerInside = true;
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		isPointerInside = false;
	}

	public void SetRenderSize(float renderSize)
	{
		orthoSize = Mathf.Clamp(renderSize, minOrthoSize, maxOrthoSize);
		cartesianRenderer.toolTipRenderSize = orthoSize / baseOrthoSize;
	}
}
