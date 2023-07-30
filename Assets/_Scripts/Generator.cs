using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public readonly int maxHealth = 20;
    int health;
    public int Health {
        get {
            return health;
		}
        set {

            health = value;
            if(health > maxHealth) health = maxHealth;
            else if(health < 0) Stop();

        }
    }

    void Start() {
        
        health = maxHealth;

    }

    void Stop() {



	}

	void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Player>().Interactable = gameObject;
		else if(collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<Enemy>().generator = this;

	}

}
