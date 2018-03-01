using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrixRenderManager : MonoBehaviour
{
	public CartesianRender cartesianRenderer;

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

	public void ResetAnimationBars()
	{
		for (int i = 0; i < animationBars.Length; i++)
		{
			animationBars[i].fillAmount = 0f;
		}
	}

	public void RenderUnTransformed()
	{
		StopAllCoroutines();
		cartesianRenderer.TransformPoints(Matrix2x2.IdentityMatrix);
		ResetAnimationBars();
    }

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
