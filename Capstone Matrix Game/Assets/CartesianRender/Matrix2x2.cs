using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <see cref="Matrix2x2"/> is a data structure that represents a 2-space Matrix.
/// </summary>
[System.Serializable]
public class Matrix2x2
{
	// a, b
	// c, d
	public readonly float a;
	public readonly float b;
	public readonly float c;
	public readonly float d;

	public static readonly Matrix2x2 IdentityMatrix = new Matrix2x2(1, 0, 0, 1);

	public Matrix2x2()
	{
		a = 0;
		b = 0;
		c = 0;
		d = 0;
	}

	public Matrix2x2(float a, float b, float c, float d)
	{
		this.a = a;
		this.b = b;
		this.c = c;
		this.d = d;
	}

	public Matrix2x2 Multiply(Matrix2x2 other)
	{
		return new Matrix2x2
			(
			this.a * other.a + this.b * other.c,
			this.a * other.b + this.b * other.d,
			this.c * other.a + this.d * other.c,
			this.c * other.b + this.d * other.d
			);
	}

    /// <summary>
    /// Compares the values inside of two <see cref="Matrix2x2"/> and returns a bool.
    /// </summary>
    /// <param name="firstMatrix"></param>
    /// <param name="secondMatrix"></param>
    /// <returns>Boolean denoting if the <see cref="Matrix2x2"/> are equal or not.</returns>
    public static bool IsEqual(Matrix2x2 firstMatrix, Matrix2x2 secondMatrix)
    {
        bool areEqual = false;

        if ((firstMatrix.a == secondMatrix.a) && (firstMatrix.b == secondMatrix.b)
            && (firstMatrix.c == secondMatrix.c) && (firstMatrix.d == secondMatrix.d))
        {
            areEqual = true;
        }

        return areEqual;
    }

    /// <summary>
    /// Returns a string representing a <see cref="Matrix2x2"/> in the following format:
    /// <para>
    ///     [ a, b ]
    ///     [ c, d ]
    /// </para>
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return "[ " + a + ", " + b + " ]\n[ " + c + ", " + d + " ]";
    }
}
