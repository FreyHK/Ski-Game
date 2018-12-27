using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    Vector3 offset;

    private void Awake() {
        offset = transform.position - target.position;
    }

    void Update () {
        Vector3 pos = new Vector3(target.position.x + offset.x, target.position.y + offset.y, -10f);
        transform.position = pos;
	}
}
