using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    [Header("Variables")]
    public float speed;
    public float maxAngleOffset = 45f;

    [Header("References")]
    public Transform target;
    public Generator generator;
    Rigidbody2D rb;

    void Start() {

        rb = GetComponent<Rigidbody2D>();
        
    }

    void Update() {
        
        Move();
        Damage();

    }

    void Move() {

        if(generator != null) return;

        float angle = Vector2.SignedAngle(Vector2.up, target.position - gameObject.transform.position);
        float offset = (Mathf.Sin(2f * Time.time) + Mathf.Sin(Time.time * Mathf.PI)) / 2f * maxAngleOffset;

        rb.velocity = Quaternion.Euler(0, 0, angle + offset) * Vector2.up * speed;

	}

    void Damage() {

        if(generator == null) return;

        generator.Health -= 1;

	}

    public void Die() {

        Destroy(gameObject);

	}

}
