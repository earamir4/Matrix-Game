﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrixRenderManager : MonoBehaviour
{
	public CartesianRender cartesianRenderer;
	public GameManager gameManager;

	private Matrix2x2[] transformationMatrices;
	private Matrix2x2 finalMatrix;

	public float animationDurationPerMatrix;
	private float animationTime;

	public Image[] animationBars;

	public void Start()
	{
		//render base points
		if (cartesianRenderer != null)
		{
			cartesianRenderer.RenderBasePoints();
		}
		ResetAnimationBars();
    }

	//tgives the manager an array of matrices to use with the cartesian render
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

		gameManager.CheckAnswer(finalMatrix);
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
		cartesianRenderer.TransformPoints(Matrix2x2.IdentityMatrix);
		ResetAnimationBars();
    }

	public void RenderPartiallyTransformed(int finalMatrix)
	{
		if (transformationMatrices == null)
		{
			Debug.Log("Can't show partial transformation with no transformation.");
			return;
		}

		if (finalMatrix >= transformationMatrices.Length)
		{
			Debug.Log("Can't render partially transformed at matrix " + finalMatrix + ", since there are only " + transformationMatrices.Length + " matrices input.");
			return;
		}

		StopAllCoroutines();

		Matrix2x2 partialTransformationMatrix = transformationMatrices[0];
		for (int i = 0; i <= finalMatrix; i++)
		{
			Matrix2x2 currentMatrix = transformationMatrices[i];
			partialTransformationMatrix = currentMatrix.Multiply(partialTransformationMatrix);
		}

		cartesianRenderer.TransformPoints(partialTransformationMatrix);

		for (int i = 0; i < animationBars.Length; i++)
		{
			animationBars[i].fillAmount = 0f;

			if (i <= finalMatrix)
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

		cartesianRenderer.TransformPoints(finalMatrix);

		for (int i = 0; i < transformationMatrices.Length; i++)
		{
			animationBars[i].fillAmount = 1f;
		}
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

		cartesianRenderer.TransformPoints(Matrix2x2.IdentityMatrix);

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
				cartesianRenderer.TransformPoints(transformationInProgress.Multiply(cumulativeMatrix));

				animationBars[i].fillAmount = animationRatio;

				yield return null;
			}

			cumulativeMatrix = cumulativeMatrix.Multiply(currentTargetMatrix);
        }

		cartesianRenderer.TransformPoints(finalMatrix);
		print("Animation finished.");
	}
}
