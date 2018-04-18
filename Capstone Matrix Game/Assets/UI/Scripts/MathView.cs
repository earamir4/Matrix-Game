using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathView : MonoBehaviour {

    public GameObject[] vectorPanels;
    public int pointX, pointY;
    public Text equation;
    private Matrix2x2[] transformationMatrices;

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

    public void SetPoint(string x, string y)
    {
        pointX = int.Parse(x);
        pointY = int.Parse(y);
    }

    public void SetEquation()
    {
        Vector2 Vect1, Vect2;

        for (int i = 0; i < transformationMatrices.Length; i++)
        {
            equation = vectorPanels[i].GetComponentInChildren<Text>();

            Vect1 = new Vector2(transformationMatrices[i].a, transformationMatrices[i].b);
            Vect2 = new Vector2(transformationMatrices[i].c, transformationMatrices[i].d);

            equation.text = pointX + " * " + Vect1.ToString() + " + " + pointY + " * " + Vect2.ToString() + " = " + ((pointX * Vect1) + (pointY * Vect2));
        }
    }
}
