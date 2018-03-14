using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrixInputTemplate : MonoBehaviour
{
    public int xSize = 2, ySize = 2;
    public GameObject[] matrixObjects;
    public InputField inputField;

    private float[] matrixValues;
    public GameObject blocker;

	public delegate void InputSlotChangedHandler();
	public event InputSlotChangedHandler inputSlotChanged;

    private void Start()
    {
        matrixValues = new float[xSize * ySize];
        GatherValuesFromText();
    }
    
    public void OnEndEdit()
    {
		inputSlotChanged();
	}
    
    private void GatherValuesFromText()
    {
        for(int i = 0; i < matrixValues.Length; i++)
        {
			String text = matrixObjects[i].GetComponentInChildren<InputField>().text;
            if (text.Length != 0)
            {
                if (text.Contains("/"))
                {
                    string[] splits = text.Split('/');
                    matrixValues[i] = float.Parse(splits[0]) / float.Parse(splits[1]);
                }
				else
				{
					matrixValues[i] = float.Parse(text);
				}
			}
        }
    }

	public void SetAcceptingInput(bool isAcceptingInput)
    {
        blocker.SetActive(!isAcceptingInput);
    }

    public float[] GetValues()
    {
		GatherValuesFromText();
        return matrixValues;
    }
}
