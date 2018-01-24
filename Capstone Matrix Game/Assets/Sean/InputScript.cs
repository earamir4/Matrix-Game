using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputScript : MonoBehaviour {

    public InputField xPos, yPos, zPos, xRot, yRot, zRot, xScale, yScale, zScale;
    public GameObject cube;
    public GameObject square;

	// Use this for initialization
	void Start () {
        zPos.text = "0";
        zRot.text = "0";
        zScale.text = "0";
        xPos.text = square.transform.localPosition.x.ToString();
        yPos.text = square.transform.localPosition.y.ToString();
        //zPos.text = cube.transform.localPosition.z.ToString();
        xRot.text = square.transform.localRotation.x.ToString();
        yRot.text = square.transform.localRotation.y.ToString();
        //zRot.text = cube.transform.localRotation.z.ToString();
        xScale.text = square.transform.localScale.x.ToString();
        yScale.text = square.transform.localScale.y.ToString();
        //zScale.text = cube.transform.localScale.z.ToString();
    }

    // Update is called once per frame
    public void UpdateCube () {
        square.transform.localPosition = new Vector3(float.Parse(xPos.text), float.Parse(yPos.text), float.Parse(zPos.text));
        square.transform.localRotation = Quaternion.Euler(new Vector3(float.Parse(xRot.text), float.Parse(yRot.text), float.Parse(zRot.text)));
        square.transform.localScale = new Vector3(float.Parse(xScale.text), float.Parse(yScale.text), float.Parse(zScale.text));

        /*
        cube.transform.localPosition = new Vector3(float.Parse(xPos.text), float.Parse(yPos.text), float.Parse(zPos.text));
        cube.transform.localRotation = Quaternion.Euler(new Vector3(float.Parse(xRot.text), float.Parse(yRot.text), float.Parse(zRot.text)));
        cube.transform.localScale = new Vector3(float.Parse(xScale.text), float.Parse(yScale.text), float.Parse(zScale.text));
        */
    }
}
