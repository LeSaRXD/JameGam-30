using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour {

    [Range(0f, 50f)]
    public float speed = 10;
    public int maxCollisions = 3;

    Rigidbody2D rb;
    int collisions = 0;

    void Start() {

        rb = gameObject.GetComponent<Rigidbody2D>();

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rb.velocity = cursorPos.normalized * speed;

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.CompareTag("Enemy")) {
            
            collision.gameObject.GetComponent<Enemy>().Die();
            if (++collisions >= maxCollisions) Destroy(gameObject);

            return;

        }
        Destroy(gameObject);

    }

}
