using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public GameObject itemPrefab;
    GameObject interactable;

    public GameObject Interactable {
        get {
            return interactable;
        }
        set {
            if(interactable == null) interactable = value;
		}
    }
    GameObject heldItem;
    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {

        Move();
        if(Input.GetMouseButtonDown(0)) ThrowItem();
        else if(Input.GetMouseButtonDown(1)) Interact();

    }

    void Move() {

        rb.position += new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized * speed * Time.unscaledDeltaTime;
        if(heldItem != null) heldItem.transform.position = gameObject.transform.position;

    }

    void ThrowItem() {

        if(heldItem == null) return;
        heldItem.GetComponent<Cog>().Throw();
        heldItem = null;

	}

    void Interact() {

        if(interactable == null) return;

        if(heldItem == null) {

            if(!interactable.CompareTag("Tree")) return;
            
            if(interactable.GetComponent<Tree>().Harvest()) heldItem = Instantiate(itemPrefab, gameObject.transform);

            return;

		}

        if(!interactable.CompareTag("Generator")) return;

        interactable.GetComponent<Generator>().Health += 2;

        Destroy(heldItem);
        heldItem = null;

        return;

	}

    public void RemoveInteractable(GameObject oldInteractable) {

        if(interactable == oldInteractable) interactable = null;

    }

}
