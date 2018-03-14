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

    private void Start()
    {
        matrixValues = new float[xSize * ySize];
        FillValuesFromText();
    }

    public void OnValueChanged()
    {
        if (inputField.text.Length <= 10) // Length restriction
        {
            if (inputField.text.Length > 0) // this is for safe side if we remove everything from inputField
            {
                if (!char.IsDigit(inputField.text[inputField.text.Length - 1])) // Only allows digits and /
                {
                    if (!inputField.text[inputField.text.Length - 1].Equals('/'))
                    {
                        inputField.text = inputField.text.Substring(0, inputField.text.Length - 1); // Remove char
                    }
                }
            }
        }
        else {
            inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
        }

    }
    
    public void OnEndEdit()
    {
        if (inputField.text.Contains("/"))
        {
            string[] splits = inputField.text.Split('/');
            inputField.text = (float.Parse(splits[0]) / float.Parse(splits[1])).ToString();
        }
    }

    private void FillValuesFromText()
    {
        for(int i = 0; i < matrixValues.Length; i++)
        {
            if (matrixObjects[i].GetComponentInChildren<InputField>().text.Length != 0)
            {
                if (matrixObjects[i].GetComponentInChildren<InputField>().text.Contains("/"))
                {
                    string[] splits = matrixObjects[i].GetComponentInChildren<InputField>().text.Split('/');
                    matrixValues[i] = float.Parse(splits[0]) / float.Parse(splits[1]);
                }
            }
            else
                matrixValues[i] = float.Parse(matrixObjects[i].GetComponentInChildren<InputField>().text);
        }
    }

    public void UpdateValues()
    {
		FillValuesFromText();
    }

    public void SetAcceptingInput(bool isAcceptingInput)
    {
        blocker.SetActive(!isAcceptingInput);
    }

    public float[] GetValues()
    {
        return matrixValues;
    }
}
