using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Variables")]
    public float speed = 8f;
    public int fixAmount = 5;

    [Header("References")]
    public GameObject itemPrefab;
    // [HideInInspector]
    public List<GameObject> interactables;

    GameObject heldItem;
    Rigidbody2D rb;

    public enum Direction { Up, Down, Left, Right };
    public Direction direction = Direction.Up;

    void Start() {

        rb = GetComponent<Rigidbody2D>();
        interactables = new List<GameObject>();

    }

    void Update() {

        Move();

        if(Input.GetMouseButtonDown(0)) ThrowItem();
        else if(Input.GetMouseButtonDown(1)) Interact();

    }

    void Move() {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        rb.position += new Vector2(horizontal, vertical).normalized * speed * Time.unscaledDeltaTime;
        if(heldItem != null) heldItem.transform.position = gameObject.transform.position;

        if(vertical == 0 && horizontal == 0) return;
        if(Mathf.Abs(horizontal) >= Mathf.Abs(vertical))
            direction = (horizontal > 0) ? Direction.Right : Direction.Left;
        else direction = (vertical > 0) ? Direction.Up : Direction.Down;

    }

    void ThrowItem() {

        if(heldItem == null) return;
        heldItem.GetComponent<Cog>().Throw();
        heldItem = null;

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
