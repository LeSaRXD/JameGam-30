using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed;
    public GameObject heldItem;

    void Start() {
        


    }

    void Update() {
        
        Move();

    }

    void Move() {

	}

    void PickUpItem(GameObject newItem) {

        if(heldItem == null) heldItem = newItem;

	}

    void ThrowItem() {

	}

    void UseItem() {



	}

}
