using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathView : MonoBehaviour {

    public Text[] vectorPanelsText;
    public GameObject inputFieldX, inputFieldY;
    public int pointX, pointY;

    private Text equation;
    private Matrix2x2[] transformationMatrices;

    private void Start()
    {
        pointX = 0;
        pointY = 0;

        SetPoint();
    }

    public void SetMatrices(Matrix2x2[] matricesArray)
    {
        if (matricesArray == null)
        {
            Debug.LogError("Math view can't accept null array of input matrices.");
            return;
        }

        transformationMatrices = matricesArray;
        SetEquation();
    }

    public void SetPoint()
    {
        pointX = int.Parse(inputFieldX.GetComponent<Text>().text);
        pointY = int.Parse(inputFieldY.GetComponent<Text>().text);
    }

    public void SetEquation()
    {
        Vector2 Vect1, Vect2;
        Vector2 pointVector = new Vector2(pointX, pointY);

        for (int i = 0; i < transformationMatrices.Length; i++)
        {
            Vect1 = new Vector2(transformationMatrices[i].a, transformationMatrices[i].b);
            Vect2 = new Vector2(transformationMatrices[i].c, transformationMatrices[i].d);

            vectorPanelsText[i].text = pointX + " * " + Vect1.ToString() + " + " + pointY + " * " + Vect2.ToString() + " = " + ((pointX * Vect1) + (pointY * Vect2));
            pointVector = ((pointX * Vect1) + (pointY * Vect2));
        }
    }
}
