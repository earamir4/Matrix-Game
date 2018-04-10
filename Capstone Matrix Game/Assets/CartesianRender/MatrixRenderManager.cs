using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrixRenderManager : MonoBehaviour
{
	public CartesianRender mainRenderer;
	public CartesianRender optionalRenderer;

	public CartesianRender previewRenderer;

	public GameManager gameManager;

	private Matrix2x2[] transformationMatrices;
	private Matrix2x2 finalMatrix;

	public float animationDurationPerMatrix;
	private float animationTime;

	public Image[] animationBars;

	public void Start()
	{
		mainRenderer.CreatePointObjects();
		optionalRenderer.CreatePointObjects();
        previewRenderer.CreatePointObjects();
			
		mainRenderer.SetRenderingDisabled(false);
		optionalRenderer.SetRenderingDisabled(true);
		previewRenderer.SetRenderingDisabled(false);

		ResetAnimationBars();
    }

	//gives the manager an array of matrices to use with the cartesian render
	//it calculates the final cumulative transformation matrix
	public void SetMatrices(Matrix2x2[] matricesArray)
	{
		if (matricesArray == null)
		{
			Debug.LogError("Render manager can't accept null array of input matrices.");
			return;
		}

		transformationMatrices = matricesArray;

		//calculate final matrix
		if (transformationMatrices.Length == 0)
		{
			finalMatrix = Matrix2x2.IdentityMatrix;
		}
		else
		{
			finalMatrix = transformationMatrices[0];
			for (int i = 1; i < transformationMatrices.Length; i++)
			{
				Matrix2x2 currentMatrix = transformationMatrices[i];
				finalMatrix = currentMatrix.Multiply(finalMatrix);
			}
		}
	}

	//tell the renders with tool tips to render them at the given scale
	public void SetToolTipRenderSize(float toolTipRenderSize)
	{
		mainRenderer.SetPointTooltipSize(toolTipRenderSize);
		optionalRenderer.SetPointTooltipSize(toolTipRenderSize);
		previewRenderer.SetPointTooltipSize(toolTipRenderSize);
	}

	//reset the animation bars each to empty
	public void ResetAnimationBars()
	{
		for (int i = 0; i < animationBars.Length; i++)
		{
			animationBars[i].fillAmount = 0f;
		}
	}

	//tell the cartesian render to render the points in their original, untransformed positions
	public void RenderUnTransformed()
	{
		StopAllCoroutines();
		mainRenderer.TransformPoints(Matrix2x2.IdentityMatrix);
		optionalRenderer.TransformPoints(Matrix2x2.IdentityMatrix);
		ResetAnimationBars();
    }

	//renders the main render with only some of the matrices, from 0 -> lastMatrixIndexBeingRendered (e.g. 0-3)
	public void RenderPartiallyTransformed(int lastMatrixIndexBeingRendered)
	{
		if (transformationMatrices == null)
		{
			Debug.Log("Can't show partial transformation with no transformation.");
			return;
		}

		if (lastMatrixIndexBeingRendered >= transformationMatrices.Length)
		{
			Debug.Log("Can't render partially transformed at matrix " + lastMatrixIndexBeingRendered + ", since there are only " + transformationMatrices.Length + " matrices input.");
			return;
		}

		StopAllCoroutines();

		Matrix2x2 partialTransformationMatrix = transformationMatrices[0];
		for (int i = 0; i <= lastMatrixIndexBeingRendered; i++)
		{
			Matrix2x2 currentMatrix = transformationMatrices[i];
			partialTransformationMatrix = currentMatrix.Multiply(partialTransformationMatrix);
		}

		mainRenderer.TransformPoints(partialTransformationMatrix);
		optionalRenderer.TransformPoints(partialTransformationMatrix);

		for (int i = 0; i < animationBars.Length; i++)
		{
			animationBars[i].fillAmount = 0f;

			if (i <= lastMatrixIndexBeingRendered)
			{
				animationBars[i].fillAmount = 1f;
			}
		}
	}

	//tell the cartesian render to render the points with the complete cumulative transformation, ending all current animations
	public void RenderFullyTransformed()
	{
		StopAllCoroutines();

		if (transformationMatrices == null || transformationMatrices.Length == 0)
		{
			Debug.Log("Can't show animation with no transformation.");
			return;
		}

		mainRenderer.TransformPoints(finalMatrix);
		optionalRenderer.TransformPoints(finalMatrix);

		for (int i = 0; i < transformationMatrices.Length; i++)
		{
			animationBars[i].fillAmount = 1f;
		}

		gameManager.CheckAnswer(finalMatrix);
	}

	//begin the animation coroutine
	//does some cleanup first
	public void StartAnimation()
	{
		if (transformationMatrices == null || transformationMatrices.Length == 0)
		{
			Debug.Log("Can't show animation with no transformation.");
			return;
		}
		StopAllCoroutines();
		StartCoroutine("TransformationAnimation");
    }

	//a coroutine that performs an animation of each matrix transformation one by one
	public IEnumerator TransformationAnimation()
	{
		print("Animation starting.");

		ResetAnimationBars();

		mainRenderer.TransformPoints(Matrix2x2.IdentityMatrix);
		optionalRenderer.TransformPoints(Matrix2x2.IdentityMatrix);

		Matrix2x2 cumulativeMatrix = Matrix2x2.IdentityMatrix;

		for (int i = 0; i < transformationMatrices.Length; i++)
		{
			print("Beginning a new transformation.");
			animationTime = 0f;
			Matrix2x2 currentTargetMatrix = transformationMatrices[i];

			while (animationTime < animationDurationPerMatrix)
			{
				animationTime += Time.deltaTime;

				float animationRatio = animationTime / animationDurationPerMatrix;

				Matrix2x2 transformationInProgress = new Matrix2x2
					(
					Mathf.Lerp(1, currentTargetMatrix.a, animationRatio),
					Mathf.Lerp(0, currentTargetMatrix.b, animationRatio),
					Mathf.Lerp(0, currentTargetMatrix.c, animationRatio),
					Mathf.Lerp(1, currentTargetMatrix.d, animationRatio)
					);
				mainRenderer.TransformPoints(transformationInProgress.Multiply(cumulativeMatrix));
				optionalRenderer.TransformPoints(transformationInProgress.Multiply(cumulativeMatrix));

				animationBars[i].fillAmount = animationRatio;

				yield return null;
			}

			cumulativeMatrix = cumulativeMatrix.Multiply(currentTargetMatrix);
        }

		RenderFullyTransformed();

		print("Animation finished.");
	}

	public void SetOptionalPoint(int positionX, int positionY)
	{
		optionalRenderer.SetListOfPoints(new Vector2[] { new Vector2(positionX, positionY) });
		optionalRenderer.CreatePointObjects();

		mainRenderer.SetRenderingDisabled(true);
		optionalRenderer.SetRenderingDisabled(false);
	}

	public void RemoveOptionalPoint()
	{
		optionalRenderer.SetListOfPoints(new Vector2[] { new Vector2(0f, 0f) });
		optionalRenderer.CreatePointObjects();

		mainRenderer.SetRenderingDisabled(false);
		optionalRenderer.SetRenderingDisabled(true);
	}

	public void SetShowCoordinates(bool showCoordinates)
	{
		mainRenderer.SetShowCoordinates(showCoordinates);
		previewRenderer.SetShowCoordinates(showCoordinates);
		optionalRenderer.SetShowCoordinates(showCoordinates);
    }

}
