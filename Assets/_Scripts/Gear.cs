using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gear : MonoBehaviour {

    [Range(0f, 50f)]
    public float speed = 10;
    public int maxCollisions = 3;

    Rigidbody2D rb;
    Animator animator;
    int collisions = 0;
    bool breaking = false;

    void Start() {

        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rb.velocity = cursorPos.normalized * speed;

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.CompareTag("Enemy")) {
            
            collision.gameObject.GetComponent<Enemy>().Die();
            if (++collisions >= maxCollisions) StartCoroutine(Break());

            return;

        }

        StartCoroutine(Break());

    }

    IEnumerator Break() {
        
        breaking = true;

        rb.velocity = Vector2.zero;
        animator.SetTrigger("Break");

        yield return new WaitUntil(() => !breaking);

        Destroy(gameObject);

	}

    public void Broken() {

        breaking = false;

	}

}
