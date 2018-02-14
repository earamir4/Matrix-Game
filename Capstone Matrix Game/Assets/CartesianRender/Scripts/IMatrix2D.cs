using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// <see cref="IMatrix2D"/> is an interface which denotes values and functions that all matrices
/// should implement.
/// </summary>
public interface IMatrix2D
{
    int A { get; set; }
    int B { get; set; }
    int C { get; set; }
    int D { get; set; }

    IMatrix2D Multiply();
}
