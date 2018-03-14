using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartesianRender : MonoBehaviour
{
	public Vector2 cartesianToWorldScale;
	public RenderAreaZoom renderAreaZoom;

	/*** Rendering Points ***/

	public GameObject pointObjectPrefab;

	//a list of cartesian space points
	[SerializeField]
	private Vector2[] listOfPoints;

	//a list of connections between points
	//<0, 2> means that there is a line between point 0 on the listOfPoints and point 2
	[SerializeField]
	private IntVector2[] lineConnections;

	private GameObject[] pointObjects;
	private Vector2[] transformedPointPositions;

	public bool showCoordinates;
	public float toolTipRenderSize;

	void Start ()
	{
		RenderBasePoints();
    }
	
	//create the point objects to later move around for transformations
	public void RenderBasePoints()
	{
		DestroyExistingPoints();

		int numPoints = listOfPoints.Length;

		transformedPointPositions = new Vector2[numPoints];

		pointObjects = new GameObject[numPoints];
		for (int i = 0; i < numPoints; i++)
		{
			Vector2 pointPosition = listOfPoints[i];
			Vector3 worldPosition = new Vector3(pointPosition.x * cartesianToWorldScale[0], pointPosition.y * cartesianToWorldScale[1], -1);
            GameObject newRenderPoint = Instantiate(pointObjectPrefab, worldPosition, Quaternion.identity);

			pointObjects[i] = newRenderPoint;

			transformedPointPositions[i] = pointPosition;
        }

		ConnectLines();

		AdjustZoom();
		UpdatePointTooltips();
    }

	//using the existing point objects, transform their positions and update their tooltips based on the input transformation matrix
	public void TransformPoints(Matrix2x2 transformation)
	{
		if (transformation == null)
		{
			Debug.LogError("Can't transform points with a null transformation matrix.");
			return;
		}

		int numPoints = listOfPoints.Length;
		for (int i = 0; i < numPoints; i++)
		{
			//transform the point
			float newX = transformation.a * listOfPoints[i].x + transformation.b * listOfPoints[i].y;
			float newY = transformation.c * listOfPoints[i].x + transformation.d * listOfPoints[i].y;
            Vector2 transformedPoint = new Vector2(newX, newY);

			transformedPointPositions[i] = transformedPoint;

			//find and move the object to the point
			Vector3 worldPosition = new Vector3(transformedPoint.x * cartesianToWorldScale[0], transformedPoint.y * cartesianToWorldScale[1], -1);

			pointObjects[i].transform.position = worldPosition;
		}

		//update line positions
		for (int i = 0; i < numPoints; i++)
		{
			RenderPoint renderPoint = pointObjects[i].GetComponent<RenderPoint>();
			renderPoint.UpdateLine();
		}

		AdjustZoom();
		UpdatePointTooltips();
    }

	//destroy all existing point render objects
	private void DestroyExistingPoints()
	{
		if (pointObjects == null)
			return;

		for (int i = 0; i < listOfPoints.Length; i++)
		{
			Destroy(pointObjects[i]);
        }

		pointObjects = null;
    }

	//update each point to create the lines that connect points together
	private void ConnectLines()
	{
		if (lineConnections == null)
		{
			Debug.Log("Won't connect lines with no line connections to make.");
			return;
		}

		for (int i = 0; i < lineConnections.Length; i++)
		{
			int pointA = lineConnections[i].x;
			int pointB = lineConnections[i].y;

			if (pointA < pointObjects.Length && pointB < pointObjects.Length)
			{
				RenderPoint renderPointA = pointObjects[pointA].GetComponent<RenderPoint>();
				RenderPoint renderPointB = pointObjects[pointB].GetComponent<RenderPoint>();

				if (renderPointA != null && renderPointB != null)
				{
					renderPointA.AssignNextPointInLine(renderPointB);
				}
				else
				{
					print("While trying to render a line, could not find RenderPoint components on instantiated point objects.");
				}
            }
			else
			{
				print("Can't render a line between points " + pointA + " and " + pointB + ".");
			}
		}
	}

	//adjust the zoom level of the render area based on the extent of point positions
	private void AdjustZoom()
	{
		float maxDistance = 0;
		foreach (GameObject pointObject in pointObjects)
		{
			float xDistance = Mathf.Abs(transform.position.x - pointObject.transform.position.x);
			float yDistance = Mathf.Abs(transform.position.y - pointObject.transform.position.y);
			yDistance *= 1.4f;

			if (xDistance > maxDistance)
			{
				maxDistance = xDistance;
            }

			if (yDistance > maxDistance)
			{
				maxDistance = yDistance;
            }
		}

		float renderSize = (maxDistance * 1.2f) / cartesianToWorldScale.magnitude;
        renderAreaZoom.SetRenderSize(renderSize);
    }

	//set if coordinates should be shown above the points or not
	public void SetShowCoordinates(bool value)
	{
		Debug.Log("Setting to show coordinates: " + value.ToString());
		showCoordinates = value;
		UpdatePointTooltips();
    }

	//update the size at which the tool tips above points are rendered
	public void UpdatePointTooltipSize(float zoomSize)
	{
		Debug.Log("Tool tip render size set to: " + zoomSize.ToString("0.00"));
		toolTipRenderSize = zoomSize;
		UpdatePointTooltips();
	}

	//update each point's tooltip with text describing the point's position, and set whether its tool tip should be enabled or not
	private void UpdatePointTooltips()
	{
		//Debug.Log("Updating the tool tips for each point.");
		int numPoints = listOfPoints.Length;
		for (int i = 0; i < numPoints; i++)
		{
			RenderPoint renderPoint = pointObjects[i].GetComponent<RenderPoint>();
			renderPoint.ChangeText("(" + transformedPointPositions[i].x.ToString("0.0") + ", " + transformedPointPositions[i].y.ToString("0.0") + " )");
			renderPoint.TooltipEnabled = showCoordinates;
			renderPoint.SetToolTipSize(toolTipRenderSize);
        }
	}
}