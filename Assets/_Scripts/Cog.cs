using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cog : MonoBehaviour {

    [Range(0f, 50f)]
    public float speed = 10;
    public int maxCollisions = 3;

    Rigidbody2D rb;
    bool isThrown = false;
    int collisions = 0;

    void Start() {

        rb = gameObject.GetComponent<Rigidbody2D>();

    }

    public void Throw() {

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.parent.position;
        gameObject.transform.SetParent(null);
        rb.velocity = cursorPos.normalized * speed;

        isThrown = true;

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if(!isThrown) return;

        if(collision.gameObject.CompareTag("Enemy")) {
            
            collision.gameObject.GetComponent<Enemy>().Die();
            if (++collisions >= maxCollisions) Destroy(gameObject);

            return;

        }
        if(!collision.gameObject.CompareTag("Player")) Destroy(gameObject);

    }

}
