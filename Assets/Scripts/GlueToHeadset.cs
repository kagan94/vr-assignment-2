using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueToHeadset : MonoBehaviour {
	
    public GameObject MainCamera;
	private float distance = 2f;

    // Update is called once per frame
    void Update() {
		transform.position = MainCamera.transform.position + MainCamera.transform.forward * distance;
//        transform.rotation = MainCamera.transform.rotation;
//         transform.rotation = new Quaternion(0.0f, MainCamera.transform.rotation.y, 0.0f, MainCamera.transform.rotation.w);
    }
}
