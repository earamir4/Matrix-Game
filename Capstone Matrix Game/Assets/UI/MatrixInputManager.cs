using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixInputManager : MonoBehaviour
{
	//input spots, from first to input to last to input
	public TemplateInputSpot[] inputSpots;
	private int spotsFilled;
    public MatrixRenderManager renderManager;

	public void Start()
	{
		inputSpots[0].AcceptingInput = true;
    }

    public void SendToBackend()
    {
		List<Matrix2x2> inputMatrices = new List<Matrix2x2>();

		foreach (TemplateInputSpot templateInputSpot in inputSpots)
		{
			GameObject storedTemplateObject = templateInputSpot.storedTemplateObject;

			//check if slot is has a stored slot
			if (storedTemplateObject == null)
			{
				//if slot has nothing in it, stop pushing input since rest shouldn't have anything in them
				break;
			}

			MatrixInputTemplate workingTemplate = storedTemplateObject.GetComponent<MatrixInputTemplate>();

			if (workingTemplate == null)
			{
				Debug.Log("Can't find MatrixInputTemplate component on templateInputObject stored in a slot.");
			}

			workingTemplate.UpdateValues();

			int[] inputValues = workingTemplate.GetValues();

			//TODO expand to other matrix sizes
			Matrix2x2 newMatrix = new Matrix2x2(inputValues[0], inputValues[1], inputValues[2], inputValues[3]);

			inputMatrices.Add(newMatrix);
        }

		renderManager.SetMatrices(inputMatrices.ToArray());
    }

	public void SpotAcceptedInput()
	{
		spotsFilled++;
		if (spotsFilled < inputSpots.Length)
		{
			inputSpots[spotsFilled].AcceptingInput = true;
        }
    }
}
