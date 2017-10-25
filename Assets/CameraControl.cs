using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

	private float offsetY = 2f;

	// Use this for initialization
	void Start () {
		transform.position = transform.position + transform.up * offsetY;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
