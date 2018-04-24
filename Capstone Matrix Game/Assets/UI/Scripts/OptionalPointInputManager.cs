﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionalPointInputManager : MonoBehaviour
{
	public InputField fieldX;
	public InputField fieldY;
	public MatrixRenderManager renderManager;
	public Button clearButton;
    private MathView mathView;

	public void Start()
	{
		renderManager = GameObject.FindObjectOfType<MatrixRenderManager>();
		clearButton.interactable = false;
		mathView = FindObjectOfType<MathView>();
    }

	public void ClearInput()
	{
		fieldX.text = "";
		fieldY.text = "";
		renderManager.RemoveOptionalPoint();
		clearButton.interactable = false;
		mathView.SetPoint(0, 0);
	}

	public void TrySubmitOptionalPoint()
	{
		if (fieldX.text != "" && fieldY.text != "")
		{
			renderManager.SetOptionalPoint(int.Parse(fieldX.text), int.Parse(fieldY.text));
			clearButton.interactable = true;
            mathView.SetPoint(int.Parse(fieldX.text), int.Parse(fieldY.text));
			mathView.SetPoint(int.Parse(fieldX.text), int.Parse(fieldY.text));
		}
    }
}
