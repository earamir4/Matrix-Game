using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <see cref="MatrixLogger"/> keeps track of the text displayed when the Player
/// opens the log during gameplay.
/// </summary>
/// <remarks>
///     Author: Erick Ramirez Cordero
///     Date:   2/26/18
/// </remarks>
public class MatrixLogger: MonoBehaviour
{
    private static Text LogField;

    public void Start()
    {
        LogField = GetComponentInChildren<Text>();
    }

    /// <summary>
    /// Add information to the log
    /// </summary>
    /// <param name="data">String to add to the log</param>
    public static void Add(string data)
    {
		if (LogField == null)
		{
			Debug.LogError("Matrix Logger can't add anything to log since it doesn't have a reference to the Log Text.");
		}
        else
        {
            LogField.text += data + "\n\n";
        }
    }

    /// <summary>
    /// Clears the <see cref="MatrixLogger"/> of text.
    /// </summary>
    public static void Clear()
    {
        LogField.text = string.Empty;
    }

    /// <summary>
    /// Toggles visibility of the <see cref="MatrixLogger"/> UI.
    /// </summary>
    public void ToggleVisibility()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }
}
