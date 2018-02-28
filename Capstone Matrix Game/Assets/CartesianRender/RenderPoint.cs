using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderPoint : MonoBehaviour
{
	[SerializeField]
	public Text labelText;

	[SerializeField]
	private RenderPoint nextPointInLine;

	public void ChangeText(string text)
	{
		labelText.text = text;
    }

	public void AssignNextPointInLine(RenderPoint point)
	{
		nextPointInLine = point;
		UpdateLine();
    }

	public void UpdateLine()
	{
		if (nextPointInLine != null)
		{
			GetComponent<LineRenderer>().SetPositions(new Vector3[] { transform.position, nextPointInLine.transform.position });
		}
	}
}
