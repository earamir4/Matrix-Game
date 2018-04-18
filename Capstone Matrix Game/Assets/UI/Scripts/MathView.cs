using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathView : MonoBehaviour {

    public GameObject[] VectorPanels;
    public Text Equation;
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

    public void SetEquation()
    {
        int PointX, PointY, a, b, c, d;
        Vector2 Vect1, Vect2;

        Equation = VectorPanels[0].GetComponentInChildren<Text>();
        PointX = 1;
        PointY = 1;

        Vect1 = new Vector2 (transformationMatrices[0].a, transformationMatrices[0].b);
        Vect2 = new Vector2(transformationMatrices[0].c, transformationMatrices[0].d);

        Equation.text = PointX + " * " + Vect1.ToString() + " + " + PointY + " * " + Vect2.ToString() + " = " + ((PointX*Vect1)+(PointY*Vect2));
    }
}
