using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class IntVector2
{
	public int x
	{
		get
		{
			return X;
		}
	}
	[SerializeField]
	private int X;

	public int y
	{
		get
		{
			return Y;
		}
	}
	[SerializeField]
	private int Y;

	public IntVector2(int x, int y)
	{
		X = x;
		Y = y;
    }

	int sqrMagnitude
	{
		get
		{
			return X*X + Y*Y;
		}
	}
}
