using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cog : MonoBehaviour
{
    [Range(0f, 50f)]
    public float speed = 10;
    public int maxCollisions = 3;
    Rigidbody2D rb;
    int collisions = 0;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    public void Throw()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - gameObject.transform.parent.position;
        gameObject.transform.SetParent(null);
        rb.velocity = cursorPos.normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if (enemy != null) {
            Destroy(gameObject);
            return;
        }
        enemy.Die();
        if (++collisions >= maxCollisions) Destroy(gameObject);
    }
}
