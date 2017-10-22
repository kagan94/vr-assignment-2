using System.Collections;
using System.Collections.Generic;
using GVR.Input;
using UnityEngine;

public class Pointer : MonoBehaviour {
    public LineRenderer laser;
    public GameObject headset;
    public GameObject pointerDot;
    public GameObject target; // object that was
    public LayerMask layerMask;

    void Start() {
        Vector3[] initLaserPositions = new Vector3[2] {Vector3.zero, Vector3.zero};
        laser.SetPositions(initLaserPositions);
        laser.SetWidth(0.01f, 0.01f);
    }

    void Update() {
        Quaternion orientation = GvrControllerInput.Orientation;
        // transform.localRotation = ori;
        gameObject.transform.localRotation = orientation;

        Vector3 startPosition = transform.position;
        Vector3 direction = GvrControllerInput.Orientation * Vector3.forward;
        float length = 200f;
        
        ShootLaserFromTargetPosition(startPosition, direction, length);
        laser.enabled = true;

        // Teleportation
        if (GvrControllerInput.ClickButtonDown) {
            RaycastHit hit;
            Ray ray = new Ray(startPosition, direction);
            
            if (Physics.Raycast(ray, out hit, length, layerMask)) {
                // GameObject hitGameObject = hit.transform.gameObject;
                // Vector3 currentTargetPos = new Vector3(hit.point.x, transform.localPosition.y, hit.point.z);
                // transform.localPosition = currentTargetPos;
                Vector3 teleportToPosisition = new Vector3(hit.point.x, hit.point.y + 5, hit.point.z);
                headset.transform.position = teleportToPosisition;
            }
        }
    }

    void ShootLaserFromTargetPosition(Vector3 startPosition, Vector3 direction, float length) {
        RaycastHit hit;
        Ray ray = new Ray(startPosition, direction);

        if (Physics.Raycast(ray, out hit, length, layerMask)) {
            target = hit.transform.gameObject;
            pointerDot.transform.position = hit.point;
            pointerDot.SetActive(true);
        } else {
            target = null;
            pointerDot.SetActive(false);
        }

        Vector3 endPosition = startPosition + (length * direction);
        laser.SetPosition(0, startPosition);
        laser.SetPosition(1, endPosition);
    }
}