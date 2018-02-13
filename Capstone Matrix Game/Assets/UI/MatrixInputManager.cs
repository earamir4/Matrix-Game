using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixInputManager : MonoBehaviour
{
	[SerializeField]
    private List<MatrixInputTemplate> workingTemplates;
    public MatrixRenderManager renderManager;

	public void AddWorkingTemplate(MatrixInputTemplate newWorkingTemplate)
	{
		workingTemplates.Add(newWorkingTemplate);
    }

    public void SendToBackend()
    {
		List<Matrix2x2> inputMatrices = new List<Matrix2x2>();

		foreach (MatrixInputTemplate workingTemplate in workingTemplates)
		{
			workingTemplate.UpdateValues();

			int[] inputValues = workingTemplate.GetValues();

			//TODO expand to other matrix sizes
			Matrix2x2 newMatrix = new Matrix2x2(inputValues[0], inputValues[1], inputValues[2], inputValues[3]);
			inputMatrices.Add(newMatrix);
        }

		renderManager.SetMatrices(inputMatrices.ToArray());
    }
}
