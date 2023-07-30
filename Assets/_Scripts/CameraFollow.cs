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

        return 1 - Mathf.Pow(x - 1, 2f);

	}

    float map(float value, float min1, float max1, float min2, float max2) {
        
        return f((value - min1) / (max1 - min1)) * (max2 - min2) + min2;

    }

	void Update() {
        
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

        float
            x = map(player.position.x, leftEdge, rightEdge, leftEdge + halfWidth, rightEdge - halfWidth),
            y = map(player.position.y, bottomEdge, topEdge, bottomEdge + halfHeight, topEdge - halfHeight);

        this.transform.position = new Vector3(x, y, -10f);

    }

}
