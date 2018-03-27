using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrixInputManager : MonoBehaviour
{
	//input spots, from first to input to last to input
	public TemplateInputSpot[] inputSpots;
	private int spotsFilled;
    public MatrixRenderManager renderManager;

	//restrict ability to insert matrices to 
	public bool restrictInputToInOrder;
	public bool disableSubmissionIfSkipSlots;

	public Button submitButton;
	public Button animateButton;

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

		//subscribe to all input template slot changed events
		MatrixInputTemplate[] inputTemplates = GameObject.FindObjectsOfType<MatrixInputTemplate>();
		foreach (MatrixInputTemplate inputTemplate in inputTemplates)
		{
			inputTemplate.inputSlotChanged += new MatrixInputTemplate.InputSlotChangedHandler(TemplateInputChangedHandler);
        }

		submitButton.interactable = false;
	}

    private void SendToBackend()
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

			float[] inputValues = workingTemplate.GetValues();
			Debug.Log(inputValues);

			Matrix2x2 newMatrix = new Matrix2x2(inputValues[0], inputValues[1], inputValues[2], inputValues[3]);
			Debug.Log(newMatrix);

			inputMatrices.Add(newMatrix);
        }

		renderManager.SetMatrices(inputMatrices.ToArray());
	}

	public void SubmitMatrices()
	{
		SendToBackend();

		submitButton.interactable = false;
		animateButton.interactable = true;

		renderManager.RenderFullyTransformed();
    }

	public void BeginAnimation()
	{
		renderManager.StartAnimation();
    }

	public void AnimationSeek(int finalMatrixRendered)
	{
		renderManager.RenderPartiallyTransformed(finalMatrixRendered);
    }

	public void WorkspaceChanged()
	{
		renderManager.RenderUnTransformed();

		submitButton.interactable = true;
		animateButton.interactable = false;

		if (restrictInputToInOrder)
		{
			//goingfrom left to right along the input spots, determine which to have disabled and which to have enabled
			for (int i = inputSpots.Length - 1; i >= 0; i--)
			{
				//assume that it should be enabled
				inputSpots[i].AcceptingInput = true;

				//if its the right-most slot or there is an object stored in the slot already, keep it enabled and also stop iterating through
				if (inputSpots[i].storedTemplateObject != null || i == 0)
				{
					break;
				}

				//if the slot to the right of it doesn't contain anything, disable the slot
				if (inputSpots[i - 1].storedTemplateObject == null)
				{
					inputSpots[i].AcceptingInput = false;
				}
			}
		}

		//goingfrom right to left along the input spots, figure out if there are any skip slots
		bool haveEncounteredEmpty = false;
		bool haveEncounteredFull = false;
		for (int i = 0; i < inputSpots.Length; i++)
		{
			if (inputSpots[i].storedTemplateObject == null)
			{
				haveEncounteredEmpty = true;
			}
			else
			{
				haveEncounteredFull = true;
                if (haveEncounteredEmpty && disableSubmissionIfSkipSlots)
				{
					submitButton.interactable = false;
				}
			}
		}

		if (!haveEncounteredFull)
		{
			submitButton.interactable = false;
		}

	}

	//when a template input slot is changed...
	private void TemplateInputChangedHandler()
	{
		Debug.Log("A matrix input slot was changed.");
		WorkspaceChanged();
    }
}
