using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Matrix2x2
{
	// a, b
	// c, d
	public float a;
	public float b;
	public float c;
	public float d;

	public Matrix2x2(float a, float b, float c, float d)
	{
		this.a = a;
		this.b = b;
		this.c = c;
		this.d = d;
	}
}
