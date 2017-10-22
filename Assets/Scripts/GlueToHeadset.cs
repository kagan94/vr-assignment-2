using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueToHeadset : MonoBehaviour {
    public GameObject Headset;

    // Update is called once per frame
    void Update() {
        float distance = 4f;
        transform.position = Headset.transform.position + Headset.transform.forward * distance;
        transform.rotation = new Quaternion(0.0f, Headset.transform.rotation.y, 0.0f, Headset.transform.rotation.w);
    }
}