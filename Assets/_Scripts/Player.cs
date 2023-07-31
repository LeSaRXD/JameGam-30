using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Variables")]
    public float speed = 8f;
    public int fixAmount = 5;
    public Vector2 itemOffset = new Vector2(0.5f, 0.3f);
    public float throwCooldown = 0.5f;
    
    bool canRotate = true;
    Direction direction = Direction.Up;

    [Header("References")]
    public GameObject gearPrefab;
    public SpriteRenderer sprite;
    public Animator animator;
    public GameObject heldGear;

    [HideInInspector]
    public List<GameObject> interactables;
    bool holdingGear;
    bool HoldingGear {
        get {
            return holdingGear;
		}
        set {
            holdingGear = value;
            heldGear.SetActive(holdingGear);
		}
	}
    Rigidbody2D rb;



    void Start() {

        rb = GetComponent<Rigidbody2D>();
        interactables = new List<GameObject>();

        HoldingGear = false;

    }

    void Update() {

        Move();
        MoveGear();

        if(Input.GetMouseButtonDown(0)) StartCoroutine(ThrowGear());
        else if(Input.GetMouseButtonDown(1)) Interact();

    }

    void Move() {

        Vector2 input = new(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
        rb.position += input.normalized * speed * Time.unscaledDeltaTime;

        if(canRotate) {
            
            SetDirection(input);
            UpdateAnimation(input != Vector2.zero);

        }
        UpdateSprite(input != Vector2.zero);

    }
    
    void SetDirection(Vector2 vector) {
        
        if(vector == Vector2.zero) return;

        if(Mathf.Abs(vector.x) >= Mathf.Abs(vector.y))
            direction = (vector.x > 0) ? Direction.Right : Direction.Left;
        else direction = (vector.y > 0) ? Direction.Up : Direction.Down;

	}

    void UpdateSprite(bool isRunning) {
        
        animator.SetBool("Running", isRunning);
        sprite.flipX = direction == Direction.Left;
        
	}

    void UpdateAnimation(bool isRunning) {

        string animName = isRunning ? "Run_" : "Idle_";
        animName += (direction == Direction.Up) ?
            "Up" : ((direction == Direction.Down) ? "Down" : "Side");
        if(HoldingGear) animName += "_Holding";

        animator.Play(animName);

	}

    void MoveGear() {

        if(!heldGear.activeSelf) return;
        
        switch(direction) {
            case Direction.Down:
                heldGear.transform.localPosition = Vector2.down * itemOffset.y;
                break;
            case Direction.Up:
                heldGear.transform.localPosition = Vector2.up * itemOffset.y;
                break;
            case Direction.Left:
                heldGear.transform.localPosition = Vector2.left * itemOffset.x;
                break;
            case Direction.Right:
                heldGear.transform.localPosition = Vector2.right * itemOffset.x;
                break;
		}

	}
    
    void Interact() {

        if(interactables.Count == 0) return;

        foreach(GameObject interactable in interactables) {

            if(!HoldingGear) {

                if(!interactable.CompareTag("Tree")) continue;
            
                if(interactable.GetComponent<Tree>().Harvest()) {

                    HoldingGear = true;
                    break;

                }

            } else {

                if(!interactable.CompareTag("Generator")) continue;

                Generator generator = interactable.GetComponent<Generator>();
                if(generator.Health < generator.maxHealth) generator.Health += fixAmount;

                HoldingGear = false;

                break;

            }

        }

    }

    IEnumerator ThrowGear() {

        if(!HoldingGear) yield break;

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        SetDirection(cursorPos);
        canRotate = false;

        GameObject gear = Instantiate(gearPrefab);
        gear.transform.position = heldGear.transform.position;

		HoldingGear = false;

        animator.Play(
            "Throw_" + (
                (direction == Direction.Up) ?
                "Up" : ((direction == Direction.Down) ? "Down" : "Side")
            )
        );

        yield return new WaitForSecondsRealtime(throwCooldown);

        canRotate = true;

    }

}
