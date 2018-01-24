using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartesianRender : MonoBehaviour
{
	public Vector2 cartesianToWorldScale;
	public GameObject pointObjectPrefab;

	//a list of cartesian space points
	[SerializeField]
	private Vector2[] listOfPoints;

	//a list of connections between points
	//<0, 2> means that there is a line between point 0 on the listOfPoints and point 2
	[SerializeField]
	private IntVector2[] lineConnections;

	private GameObject[] pointObjects;

	void Start ()
	{
		RenderBasePoints();
    }

	public void RenderBasePoints()
	{
		DestroyExistingPoints();

		int numPoints = listOfPoints.Length;
        pointObjects = new GameObject[numPoints];
		for (int i = 0; i < numPoints; i++)
		{
			Vector3 worldPosition = new Vector3(listOfPoints[i].x * cartesianToWorldScale[0], listOfPoints[i].y * cartesianToWorldScale[1], -1);
            GameObject newRenderPoint = Instantiate(pointObjectPrefab, worldPosition, Quaternion.identity);
			pointObjects[i] = newRenderPoint;
        }

		RenderLines();
    }

	public void RenderTransformedPoints(Matrix2x2 transformation)
	{
		DestroyExistingPoints();

		int numPoints = listOfPoints.Length;
		pointObjects = new GameObject[numPoints];
		for (int i = 0; i < numPoints; i++)
		{
			//transform the point
			Vector2 transformedPoint = new Vector2(transformation.a * listOfPoints[i].x + transformation.b * listOfPoints[i].y, transformation.c * listOfPoints[i].x + transformation.d * listOfPoints[i].y);

			//make the object
			Vector3 worldPosition = new Vector3(transformedPoint.x * cartesianToWorldScale[0], transformedPoint.y * cartesianToWorldScale[1], -1);
			GameObject newRenderPoint = Instantiate(pointObjectPrefab, worldPosition, Quaternion.identity);
			pointObjects[i] = newRenderPoint;
		}

		RenderLines();
	}

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

	private void RenderLines()
	{
		if (lineConnections == null)
			return;

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
}