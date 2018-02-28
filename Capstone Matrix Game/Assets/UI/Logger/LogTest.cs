using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <see cref="LogTest"/> is a class used for testing the <see cref="MatrixLogger"/>.
/// </summary>
public class LogTest : MonoBehaviour
{
    private InputField InputField;

    public void Start()
    {
        InputField = GetComponentInChildren<InputField>();
    }

    /// <summary>
    /// Add the text in the <see cref="InputField"/> to the <see cref="MatrixLogger"/>.
    /// </summary>
    public void Add()
    {
        string input = InputField.text;
        input.Trim();

        if (!string.IsNullOrEmpty(input))
        {
            MatrixLogger.Add(input);
            InputField.text = string.Empty;
        }
    }

    /// <summary>
    /// Clear the <see cref="MatrixLogger"/> and <see cref="InputField"/>.
    /// </summary>
    public void Clear()
    {
        MatrixLogger.Clear();
        InputField.text = string.Empty;
    }

    /// <summary>
    /// Print a <see cref="Matrix2x2"/> to the <see cref="MatrixLogger"/>.
    /// </summary>
    public void PrintMatrix()
    {
        Matrix2x2 matrix = new Matrix2x2();
        MatrixLogger.Add(matrix.ToString());
    }
}
