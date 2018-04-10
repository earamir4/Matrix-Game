using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionalPointInputManager : MonoBehaviour
{
	public InputField fieldX;
	public InputField fieldY;
	public MatrixRenderManager renderManager;
	public Button clearButton;

	public void Start()
	{
		renderManager = GameObject.FindObjectOfType<MatrixRenderManager>();
    }

	public void ClearInput()
	{
		fieldX.text = "";
		fieldY.text = "";
		renderManager.RemoveOptionalPoint();
		clearButton.interactable = false;
    }

	public void TrySubmitOptionalPoint()
	{
		if (fieldX.text != "" && fieldY.text != "")
		{
			renderManager.SetOptionalPoint(int.Parse(fieldX.text), int.Parse(fieldY.text));
			clearButton.interactable = true;
		}
    }
}
