using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class TrackpadMovement : MonoBehaviour {

	public GameObject mainCamera;
	private LineRenderer lineRenderer;
	private float deadzoneRadius = 0.3f;
	private float movingSpeed = 0.1f;


	void Start () {
		lineRenderer = gameObject.GetComponent<LineRenderer> ();
		lineRenderer.startWidth = 0.03f;
		lineRenderer.endWidth = 0.03f;
	}
	
	// Update is called once per frame
	void Update () {
		
		Vector3 camDirectionOnPlane = Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up).normalized;
		Vector2 camDirectionOnPlane2D = new Vector2 (camDirectionOnPlane.x, camDirectionOnPlane.z);

		lineRenderer.SetPosition(0, camDirectionOnPlane * 3);
		lineRenderer.SetPosition(1, new Vector3(0, 0, 0)); //for visualizing the headset direction

		if (GvrControllerInput.IsTouching) {
			Vector2 touchPos = GvrControllerInput.TouchPosCentered;
			float distanceToCenter = Vector2.Distance (touchPos, new Vector2 (0, 0));
			if(distanceToCenter >= deadzoneRadius) 
			{
				double rotationRadianRelatedToYaxis = radPOX (touchPos.x, touchPos.y) - (Math.PI / 2);
				Vector2 newPosition2D = RotatePoint (camDirectionOnPlane2D, rotationRadianRelatedToYaxis, false);
				Vector3 newPosition = new Vector3 (newPosition2D.x, 0, newPosition2D.y);
				lineRenderer.SetPosition(2, newPosition); //for visualizing the touchpad offset direction

				transform.position = transform.position + (newPosition * movingSpeed);

				print("transform position=" + transform.position);//debug
			}
		}	
	}


	/// <summary>
	/// calculate the angle between the 2D vector and X-axis
	/// </summary>
	/// <param name="x">x position of the vector</param>
	/// <param name="y">y position of the vector</param>
	/// <returns>angle in radian</returns>
	private static double radPOX(double x,double y)
	{
		//when P is (0, 0)
		if (x == 0 && y == 0) return 0;

		//different cases when P is on x, y, z axis
		if (y == 0 && x > 0) return 0;
		if (y == 0 && x < 0) return Math.PI;
		if (x == 0 && y > 0) return Math.PI / 2;
		if (x == 0 && y < 0) return Math.PI / 2 * 3;

		//different cases when P is in 1st, 2nd, 3rd and 4th quadrant
		if (x > 0 && y > 0) return Math.Atan(y / x);
		if (x < 0 && y > 0) return Math.PI - Math.Atan(y / -x);
		if (x < 0 && y < 0) return Math.PI + Math.Atan(-y / -x);
		if (x > 0 && y < 0) return Math.PI * 2 - Math.Atan(-y / x);

		return 0;
	}


	/// <summary>
	/// Calcluate the new position after rotating P by angle rad.
	/// </summary>
	/// <param name="P">the initial position</param>
	/// <param name="rad">rotation angle in radian</param>
	/// <param name="isClockwise">true/false</param>
	/// <returns>the new position after rotation</returns>
	private static Vector2 RotatePoint(Vector2 P, double rad, bool isClockwise = true)
	{
		double pLength = Vector2.Distance (P, new Vector2 (0, 0));
		double angPOX = radPOX(P.x, P.y);
		double angPOXRotated = angPOX - (isClockwise ? 1 : -1) * rad;
		Vector2 newP = new Vector2 (Convert.ToSingle (pLength * Math.Cos (angPOXRotated)), Convert.ToSingle (pLength * Math.Sin (angPOXRotated)));
		return newP;
	}


}
