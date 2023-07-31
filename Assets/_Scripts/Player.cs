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
    enum Direction { Up, Down, Left, Right };
    [SerializeField]
    Direction direction = Direction.Up;

    [Header("References")]
    public GameObject itemPrefab;

    [HideInInspector]
    public List<GameObject> interactables;
    GameObject heldItem;
    Rigidbody2D rb;



    void Start() {

        rb = GetComponent<Rigidbody2D>();
        interactables = new List<GameObject>();

    }

    void Update() {

        Move();
        MoveItem();

        if(Input.GetMouseButtonDown(0)) StartCoroutine(ThrowItem());
        else if(Input.GetMouseButtonDown(1)) Interact();

    }

    void Move() {

        Vector2 input = new(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
        rb.position += input.normalized * speed * Time.unscaledDeltaTime;
        
        if(input == Vector2.zero || !canRotate) return;
        SetDirection(input);

    }

    void SetDirection(Vector2 vector) {
        
        if(Mathf.Abs(vector.x) >= Mathf.Abs(vector.y))
            direction = (vector.x > 0) ? Direction.Right : Direction.Left;
        else direction = (vector.y > 0) ? Direction.Up : Direction.Down;

	}

    void MoveItem() {

        if(heldItem == null) return;
        
        heldItem.transform.position = gameObject.transform.position;

        switch(direction) {
            case Direction.Down:
                heldItem.transform.position += Vector3.down * itemOffset.y;
                break;
            case Direction.Up:
                heldItem.transform.position += Vector3.up * itemOffset.y;
                break;
            case Direction.Left:
                heldItem.transform.position += Vector3.left * itemOffset.x;
                break;
            case Direction.Right:
                heldItem.transform.position += Vector3.right * itemOffset.x;
                break;
		}

	}

    IEnumerator ThrowItem() {

        if(heldItem == null) yield break;

        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        SetDirection(cursorPos);
        canRotate = false;

        heldItem.GetComponent<Cog>().Throw(cursorPos);
        heldItem = null;

        yield return new WaitForSecondsRealtime(throwCooldown);

        canRotate = true;

    }

    void Interact() {

        if(interactables.Count == 0) return;

        foreach(GameObject interactable in interactables) {

            if(heldItem == null) {

                if(!interactable.CompareTag("Tree")) continue;
            
                if(interactable.GetComponent<Tree>().Harvest()) {

                    heldItem = Instantiate(itemPrefab, gameObject.transform);
                    break;

                }

            } else {

                if(!interactable.CompareTag("Generator")) continue;

                Generator generator = interactable.GetComponent<Generator>();
                if(generator.Health < generator.maxHealth) generator.Health += fixAmount;

                Destroy(heldItem);
                heldItem = null;

                break;

            }

        }

    }

}
