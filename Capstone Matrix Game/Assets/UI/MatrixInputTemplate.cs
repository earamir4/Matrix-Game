using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatrixInputTemplate : MonoBehaviour
{
    public int xSize = 2, ySize = 2;
    public InputField xField, yField;
    public GameObject[] matrixObjects;

    private int[] matrixValues;
    public GameObject blocker;

    private void Start()
    {
        matrixValues = new int[xSize * ySize];
        xField.text = xSize.ToString();
        yField.text = ySize.ToString();
        FillValuesFromText();
    }

    private void FillValuesFromText()
    {
        for(int i = 0; i < matrixValues.Length; i++)
        {
            matrixValues[i] = int.Parse(matrixObjects[i].GetComponentInChildren<InputField>().text);
        }
    }

    public void UpdateValues()
    {
		FillValuesFromText();
    }

    public void SetAcceptingInput(bool isAcceptingInput)
    {
        blocker.SetActive(isAcceptingInput);
    }

    public int[] GetValues()
    {
        return matrixValues;
    }

    public void UpdateSizeX(string x)
    {
        xSize = int.Parse(x);
        matrixValues = new int[xSize * ySize];
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((xSize * 100), ySize * 100);
    }

    public void UpdateSizeY(string y)
    {
        ySize = int.Parse(y);
        matrixValues = new int[xSize * ySize];
        gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2((xSize * 100), ySize * 100);
    }

}
