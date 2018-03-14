using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderPoint : MonoBehaviour
{
	public Text labelText;
	public RectTransform canvasRect;
	public float baseCanvasScale;

	[SerializeField]
	private RenderPoint nextPointInLine;

	public bool TooltipEnabled
	{
		get
		{
			return tooltipEnabled;
        }
		set
		{
			tooltipEnabled = value;
			if (tooltipEnabled)
			{
				canvasRect.gameObject.SetActive(true);
            }
			else
			{
				canvasRect.gameObject.SetActive(false);
			}
        }
	}
	private bool tooltipEnabled;

	public void ChangeText(string text)
	{
		labelText.text = text;
		SetToolTipSize(1f);
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

	public void SetToolTipSize(float size)
	{
		canvasRect.localScale = Vector3.one * size * 0.03f;
    }
}
