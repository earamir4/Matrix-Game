using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyRenderManager : MonoBehaviour
{
	public CartesianRender cartesianRenderer;

	public Matrix2x2 transformationMatrix;

	public bool renderTransformed;
	private bool renderTransformedPreviousFrame;

	public void Start()
	{
		if (cartesianRenderer != null)
		{
			cartesianRenderer.RenderBasePoints();
		}
	}

	public void Update()
	{
		if (cartesianRenderer != null && (renderTransformed != renderTransformedPreviousFrame))
		{
			if (renderTransformed)
			{
				//hard coded matrix values for now
				cartesianRenderer.RenderTransformedPoints(transformationMatrix);
			}
			else
			{
				cartesianRenderer.RenderBasePoints();
			}
		}

		renderTransformedPreviousFrame = renderTransformed;
    }
}
