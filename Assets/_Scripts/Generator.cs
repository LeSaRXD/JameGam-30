using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public readonly int maxHealth = 20;
    private int health;
    public int Health {
        get {
            return health;
        }
        set {

            if(value > maxHealth) value = maxHealth;
            
            // todo change timescale
            health = value;

		}
    }

    void Start() {
        
        Health = maxHealth;

    }

    void Update() {
        
        

    }

	void OnTriggerEnter2D(Collider2D collision) {
		
        Player player = collision.gameObject.GetComponent<Player>();
        if(player != null) {

            player.AddInteractable(gameObject);
            return;

		}
        Enemy enemy = collision.gameObject.GetComponent<Enemy>();
        if(enemy != null) {

            enemy.generator = this;
            return;

		}

	}

}
