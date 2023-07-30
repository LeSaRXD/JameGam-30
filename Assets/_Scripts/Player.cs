using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public GameObject itemPrefab;
    public GameObject interactableObject;
    GameObject heldItem;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        Move();
        if (Input.GetMouseButtonDown(0)) ThrowItem();
        if (Input.GetMouseButtonDown(1)) Interact();
    }

    void Move() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        rb.position += new Vector2(horizontal, vertical).normalized * speed * Time.unscaledDeltaTime;
        if (heldItem != null)heldItem.transform.position = gameObject.transform.position;
    }

    void ThrowItem() {
        if (heldItem == null) return;
        heldItem.GetComponent<Cog>().Throw();
        heldItem = null;
	}

    void Interact() {
        // generator
        if (interactableObject == null) return;
        Generator generator = interactableObject.GetComponent<Generator>();
        if (generator != null && heldItem != null) {
            generator.Health += 2;
            return;
        }
        // tree
        Tree tree = interactableObject.GetComponent<Tree>();
        if (heldItem == null && tree.Harvest()) {
            heldItem = Instantiate(itemPrefab, gameObject.transform);
        }
	}

    public void AddInteractable(GameObject newInteractable)
    {
        if (interactableObject == null) interactableObject = newInteractable;
    }
}
