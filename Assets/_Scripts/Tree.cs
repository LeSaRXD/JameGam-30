using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public GameObject[] cogs;
    public readonly float growCooldown;
    public float timeToGrow;

    void Start() {
        
    }

    void Update() {
        
        timeToGrow -= Time.deltaTime;
        if(timeToGrow <= 0) Grow();

    }

    void Grow() {

        // todo change cogs
        timeToGrow = growCooldown;

	}

    public bool Harvest() {

        foreach(GameObject cog in cogs) {

            if(cog.activeSelf) {

                cog.SetActive(false);
                return true;

            }

        }

        return false;

    }

	void OnTriggerEnter2D(Collider2D collision) {
		
        if(!collision.gameObject.CompareTag("Player")) return;

        collision.gameObject.GetComponent<Player>().Interactable = gameObject;

	}

    private void OnTriggerExit2D(Collider2D collision) {

        if(!collision.gameObject.CompareTag("Player")) return;

        collision.gameObject.GetComponent<Player>().RemoveInteractable(gameObject);

    }
}
