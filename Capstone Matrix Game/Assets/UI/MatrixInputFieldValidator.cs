using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class MatrixInputFieldValidator : MonoBehaviour
{
	public MatrixInputTemplate inputTemplate;
	public InputField inputField;
	public string originalText;

	//the regex for determining if an input is valid
	//checks for either nonnegative integers
	//or for fractions where the numerator is a nonnegative integer, and the denomenator is a positive integer
	public static Regex inputFieldValidatorRegex = new Regex("^(([0-9]+)|([0-9][0-9]*/0*[1-9][0-9]*))$");

	private void Start()
	{
		originalText = inputField.text;
    }

	//on value changed, check if the most recently given value is a valid value or not
	//if it isn't valid, remove it
	public void OnValueChanged()
	{
		if (inputField.text.Length <= 10) // Length restriction
		{
			if (inputField.text.Length > 0) // this is for safe side if we remove everything from inputField
			{
				if (!char.IsDigit(inputField.text[inputField.text.Length - 1])) // Only allows digits and /
				{
					if (!inputField.text[inputField.text.Length - 1].Equals('/'))
					{
						inputField.text = inputField.text.Substring(0, inputField.text.Length - 1); // Remove char
					}
				}
			}
		}
		else
		{
			inputField.text = inputField.text.Substring(0, inputField.text.Length - 1);
		}
	}

	//on the input field stopped being edited, check if its input matches with the regex
	//if it doesn't, remove the input
	public void OnEndEdit()
	{
		if (!inputFieldValidatorRegex.IsMatch(inputField.text))
		{
			inputField.text = originalText;
		}

		inputTemplate.OnEndEdit();
    }
}
