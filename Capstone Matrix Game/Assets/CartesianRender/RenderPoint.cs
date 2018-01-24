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
        GetComponent<LineRenderer>().SetPositions(new Vector3[] { transform.position, point.transform.position });
	}
}
