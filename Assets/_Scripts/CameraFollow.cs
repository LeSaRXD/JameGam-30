using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float
        leftEdge = -30f,
        rightEdge = 30f,
        bottomEdge = -17.5f,
        topEdge = 17.5f;
    private float halfWidth, halfHeight;

    private Camera cam;
    public Transform player;

	void Start() {
		
        cam = GetComponent<Camera>();

	}

    float f(float x) {

        return (3 - 2 * x) * x * x;

	}

    float map(float value, float min1, float max1, float min2, float max2) {
        
        return f((value - min1) / (max1 - min1)) * (max2 - min2) + min2;

    }

	void Update() {
        
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

        Debug.Log(cam.aspect);

        float
            x = map(player.position.x, leftEdge + 1, rightEdge - 1, leftEdge + halfWidth, rightEdge - halfWidth),
            y = map(player.position.y, bottomEdge + 1, topEdge - 1, bottomEdge + halfHeight, topEdge - halfHeight);

        this.transform.position = new Vector3(x, y, -10f);

    }

}
