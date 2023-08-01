using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    
    [Header("Variables")]
    public float speed;
    public float maxAngleOffset = 45f;
    public float damageCooldown = 3f;
    Direction direction = Direction.Up;

    [HideInInspector]
    public Transform target;
    [HideInInspector]
    public Generator generator;
    Rigidbody2D rb;
    SpriteRenderer sprite;
    Animator animator;

    float randomOffset;
    bool damaging = false;
    bool dying = false;



    void Start() {

        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        randomOffset = Random.value * 100;
        
    }

    void Update() {

        bool isRunning = generator == null;

        if(damaging || dying) return;
        
        if(isRunning) {
            
            Move();
            UpdateSprite(isRunning);

        } else StartCoroutine(Damage());

    }

    void Move() {

        float angle = Vector2.SignedAngle(Vector2.up, target.position - gameObject.transform.position);
        float t = Time.timeSinceLevelLoad + randomOffset;
        float offset = (Mathf.Sin(2f * t) + Mathf.Sin(t * Mathf.PI)) / 2f * maxAngleOffset;

        Vector2 velocity = Quaternion.Euler(0, 0, angle + offset) * Vector2.up * speed;

        rb.position += velocity * Time.deltaTime;

        if(Mathf.Abs(velocity.x) >= Mathf.Abs(velocity.y))
            direction = (velocity.x > 0) ? Direction.Right : Direction.Left;
        else direction = (velocity.y > 0) ? Direction.Up : Direction.Down;

	}

    IEnumerator Damage() {

        yield return new WaitForEndOfFrame();

        damaging = true;

        string animationString = "Damage_" + (
            (direction == Direction.Up) ?
            "Up" : ((direction == Direction.Down) ? "Down" : "Side")
        );
        animator.Play(animationString);
        
        yield return new WaitUntil(() => !damaging);

        generator.Health -= 1;

	}

    void UpdateSprite(bool isRunning) {

        sprite.flipX = direction == Direction.Left;

        string animationString = (
            isRunning ? "Run_" : "Idle_"
        ) + (
            (direction == Direction.Up) ?
            "Up" : ((direction == Direction.Down) ? "Down" : "Side")
        );

        animator.Play(animationString);

	}

    public void Die() {

        StartCoroutine(DieRoutine());

	}

    IEnumerator DieRoutine() {

        dying = true;

        string animationString = "Die_" + (
            (direction == Direction.Up) ?
            "Up" : ((direction == Direction.Down) ? "Down" : "Side")
        );
        animator.Play(animationString);
        
        yield return new WaitUntil(() => !dying);

        Destroy(gameObject);

	}

    public void StoppedDamaging() {

        if(dying) return;
		damaging = false;

	}

    public void Dead() {
        
        dying = false;

	}

}
