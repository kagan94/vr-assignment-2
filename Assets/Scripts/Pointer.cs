using System.Collections;
using System.Collections.Generic;
using GVR.Input;
using UnityEngine;

public class Pointer : MonoBehaviour {

    public GameObject headset;
    public GameObject pointerDot;
    public GameObject target;
    public LayerMask layerMask;
	public LineRenderer lineRenderer;


	void Start() {
		lineRenderer = gameObject.GetComponent<LineRenderer> ();
		lineRenderer.startWidth = 0.03f;
		lineRenderer.endWidth = 0.03f;
	}


	void Update() {

		// Rotating the controller
		Quaternion orientation = GvrControllerInput.Orientation;
		transform.rotation = orientation;

		Vector3 startPosition = transform.position;
        Vector3 direction = GvrControllerInput.Orientation * Vector3.forward;
        float length = 200f;
        

		// Raycasting
		RaycastHit hit = ShootLaserFromTargetPosition(startPosition, direction, length, pointerDot, lineRenderer);


		// Teleportation
        if (GvrControllerInput.ClickButtonDown) {
			Vector3 teleportToPosisition = new Vector3(hit.point.x, headset.transform.position.y, hit.point.z);
            headset.transform.position = teleportToPosisition;
        }
	}


	private RaycastHit ShootLaserFromTargetPosition(Vector3 startPosition, Vector3 direction, float length, GameObject dot, LineRenderer line) {
        RaycastHit hit;
        Ray ray = new Ray(startPosition, direction);

        if (Physics.Raycast(ray, out hit, length)) {
            target = hit.transform.gameObject;
			dot.transform.position = hit.point;
			dot.SetActive(true);
        } else {
            target = null;
			dot.SetActive(false);
        }

        Vector3 endPosition = startPosition + (length * direction);
		line.SetPosition(0, startPosition);
		line.SetPosition(1, endPosition);

		return hit;
    }


}