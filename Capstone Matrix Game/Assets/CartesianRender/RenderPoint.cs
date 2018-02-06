using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderPoint : MonoBehaviour
{
	[SerializeField]
	private RenderPoint nextPointInLine;

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
