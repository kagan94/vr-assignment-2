using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueToHeadset : MonoBehaviour {
    public GameObject MainCamera;

    // Update is called once per frame
    void Update() {
        float distance = 4f;
        transform.position = MainCamera.transform.position + MainCamera.transform.forward * distance;
        transform.rotation = MainCamera.transform.rotation;
        // transform.rotation = new Quaternion(0.0f, MainCamera.transform.rotation.y, 0.0f, MainCamera.transform.rotation.w);
    }
}
