using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixInputManager : MonoBehaviour
{
	//input spots, from first to input to last to input
	public TemplateInputSpot[] inputSpots;
	private int spotsFilled;
    public MatrixRenderManager renderManager;

	public bool restrictInputToInOrder;

	public void Start()
	{
		for (int i = 0; i < inputSpots.Length; i++)
		{
			if (inputSpots[i] != null)
			{
				if (i == 0 || !restrictInputToInOrder)
				{
					inputSpots[i].AcceptingInput = true;
				}
				else
				{
					inputSpots[i].AcceptingInput = false;
				}
			}
		}
	}

    public void SendToBackend()
    {
		List<Matrix2x2> inputMatrices = new List<Matrix2x2>();

		int accumulatedBlankSpots = 0;
		for (int i = 0; i<inputSpots.Length; i++)
		{
			TemplateInputSpot templateInputSpot = inputSpots[i];

            GameObject storedTemplateObject = templateInputSpot.storedTemplateObject;

			//check if slot is has a stored slot
			if (storedTemplateObject == null)
			{
				accumulatedBlankSpots++;
                continue;
			}

			MatrixInputTemplate workingTemplate = storedTemplateObject.GetComponent<MatrixInputTemplate>();

			if (workingTemplate == null)
			{
				Debug.Log("Can't find MatrixInputTemplate component on templateInputObject stored in a slot.");
			}

			if (accumulatedBlankSpots > 0)
			{
				for (int k = 0; k < accumulatedBlankSpots; k++)
				{
					inputMatrices.Add(Matrix2x2.IdentityMatrix);
				}
				accumulatedBlankSpots = 0;
            }

			workingTemplate.UpdateValues();

			int[] inputValues = workingTemplate.GetValues();

			//TODO expand to other matrix sizes
			Matrix2x2 newMatrix = new Matrix2x2(inputValues[0], inputValues[1], inputValues[2], inputValues[3]);

			inputMatrices.Add(newMatrix);
        }

		renderManager.SetMatrices(inputMatrices.ToArray());
    }

	public void UpdateInputabilityStatus()
	{
		if (!restrictInputToInOrder)
			return;

		for (int i = inputSpots.Length-1; i>=0; i--)
		{
			inputSpots[i].AcceptingInput = true;
			if (inputSpots[i].storedTemplateObject != null || i == 0)
			{
				break;
			}

			if (inputSpots[i - 1].storedTemplateObject == null)
			{
				//Debug.Log("Input spot " + (i - 1).ToString() + " empty, so disbaling input to spot " + i.ToString() + ".");
				inputSpots[i].AcceptingInput = false;
			}
		}
	}
}
