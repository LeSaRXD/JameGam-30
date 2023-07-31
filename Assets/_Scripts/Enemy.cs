using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    [Header("Variables")]
    public float speed;
    public float maxAngleOffset = 45f;
    public float damageCooldown = 3f;

    [Header("References")]
    public Transform target;
    public Generator generator;

    Rigidbody2D rb;

    float randomOffset;
    float attackCooldown;
    
    void Start() {

        rb = GetComponent<Rigidbody2D>();
        
        randomOffset = Random.value * 10;
        attackCooldown = damageCooldown;

    }

    void Update() {
        
        Move();
        Damage();

    }

    void Move() {

        if(generator != null) return;

        float angle = Vector2.SignedAngle(Vector2.up, target.position - gameObject.transform.position);
        float t = Time.timeSinceLevelLoad + randomOffset;
        float offset = (Mathf.Sin(2f * t) + Mathf.Sin(t * Mathf.PI)) / 2f * maxAngleOffset;

        Vector2 velocity = Quaternion.Euler(0, 0, angle + offset) * Vector2.up * speed;

        rb.position += velocity * Time.deltaTime;

	}

    void Damage() {

        if(generator == null) return;

        attackCooldown -= Time.deltaTime;
        if (attackCooldown > 0) return;
        
        attackCooldown = damageCooldown;
        generator.Health -= 1;

	}

    public void Die() {

        Destroy(gameObject);

	}

}
