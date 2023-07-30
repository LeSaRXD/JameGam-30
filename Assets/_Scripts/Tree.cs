using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public Animation[] cogs;
    
    public bool Harvest() {

        foreach(Animation cog in cogs) {

            if(!cog.isPlaying) {

                cog.Play();
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
