using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
		this.a = 0;
		this.b = 0;
		this.c = 0;
		this.d = 0;
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

    public override string ToString()
    {
        return "[ " + a + ", " + b + " ]\n[ " + c + ", " + d + " ]";
    }
}
