using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrixInputTemplate : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] matrixObjects;

    //Default value should be edited on the prefab
    public string[] defaultValue = new string[4];
    private float[] matrixValues = new float[4];
    public GameObject blocker;
    public bool inWorkspace = false;

	public delegate void InputSlotChangedHandler();
	public event InputSlotChangedHandler inputSlotChanged;

    private void Start()
    {

        for (int i = 0; i < defaultValue.Length; i++)
        {
            float value;
            if (float.TryParse(defaultValue[i], out value))
            {
                matrixValues[i] = float.Parse(defaultValue[i]);
                matrixObjects[i].GetComponent<InputField>().interactable = false;
                matrixObjects[i].GetComponent<InputField>().text = defaultValue[i];
            }
            else
            {
                matrixValues[i] = 0;
                matrixObjects[i].GetComponent<InputField>().interactable = true;
                matrixObjects[i].GetComponent<InputField>().text = defaultValue[i];
            }
        }    
    }
    
    public void OnEndEdit()
    {
		inputSlotChanged();
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
