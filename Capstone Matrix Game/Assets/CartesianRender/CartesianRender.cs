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

    public List<GameObject> listOfMatrices;

	public RenderAreaZoom renderAreaZoom;

	void Start ()
	{
		RenderBasePoints();
    }

    public void GetPoints(List<GameObject> matrices)
    {
        listOfMatrices = matrices;
    }

	public void RenderBasePoints()
	{
		DestroyExistingPoints();

		int numPoints = listOfPoints.Length;
        pointObjects = new GameObject[numPoints];
		for (int i = 0; i < numPoints; i++)
		{
			Vector2 pointPosition = listOfPoints[i];
			Vector3 worldPosition = new Vector3(pointPosition.x * cartesianToWorldScale[0], pointPosition.y * cartesianToWorldScale[1], -1);
            GameObject newRenderPoint = Instantiate(pointObjectPrefab, worldPosition, Quaternion.identity);
			newRenderPoint.GetComponent<RenderPoint>().ChangeText("(" + pointPosition.x.ToString("0.0") + ", " + pointPosition.y.ToString("0.0") + " )");
			pointObjects[i] = newRenderPoint;
        }

		ConnectLines();
		UpdateZoom();
    }

	public void TransformPoints(Matrix2x2 transformation)
	{
		int numPoints = listOfPoints.Length;
		for (int i = 0; i < numPoints; i++)
		{
			//transform the point
			Vector2 transformedPoint = new Vector2(transformation.a * listOfPoints[i].x + transformation.b * listOfPoints[i].y, transformation.c * listOfPoints[i].x + transformation.d * listOfPoints[i].y);

			//find and move the object to the point
			Vector3 worldPosition = new Vector3(transformedPoint.x * cartesianToWorldScale[0], transformedPoint.y * cartesianToWorldScale[1], -1);

			pointObjects[i].transform.position = worldPosition;

			RenderPoint renderPoint = pointObjects[i].GetComponent<RenderPoint>();
			renderPoint.UpdateLine();
			renderPoint.ChangeText("(" + transformedPoint.x.ToString("0.0") + ", " + transformedPoint.y.ToString("0.0") + " )");
		}

		ConnectLines();
		UpdateZoom();
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

	private void ConnectLines()
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

	private void UpdateZoom()
	{
		float maxDistance = 0;
		foreach (GameObject pointObject in pointObjects)
		{
			float xDistance = Mathf.Abs(transform.position.x - pointObject.transform.position.x);
			float yDistance = Mathf.Abs(transform.position.y - pointObject.transform.position.y);

			if (xDistance > maxDistance)
			{
				maxDistance = xDistance;
            }

			if (yDistance > maxDistance)
			{
				maxDistance = yDistance;
            }
		}

		renderAreaZoom.SetRenderSize((maxDistance * 1.2f) / cartesianToWorldScale.magnitude);
	}
}