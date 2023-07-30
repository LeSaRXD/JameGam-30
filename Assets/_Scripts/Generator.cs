using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Generator : MonoBehaviour {

    public readonly int maxHealth = 20;
    public int health;
    public Color green, yellow, red, darkRed;
    public Slider healthBar;
    public Image healthBarImage;

    public int Health {
        get {
            return health;
		}
        set {

            health = value;
            if(health <= 0) Stop();
            else if(health <= 2) UpdateTimeScale(0.25f, darkRed);
            else if(health <= 5) UpdateTimeScale(0.5f, red);
            else if(health <= 10) UpdateTimeScale(0.75f, yellow);
            else if(health <= maxHealth) UpdateTimeScale(1f, green);
            else health = maxHealth;

            healthBar.value = health;

        }
    }

    void Start() {
        
        health = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBarImage.color = green;

    }

    void Stop() {

        SceneManager.LoadScene(0);
        UpdateTimeScale(1f, green);

	}

    void UpdateTimeScale(float newTimeScale, Color newColor) {

        Time.timeScale = newTimeScale;
        Time.fixedDeltaTime = 0.02f * newTimeScale;

        healthBarImage.color = newColor;

    }

	void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Player>().Interactable = gameObject;
		else if(collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<Enemy>().generator = this;

	}

    private void OnTriggerExit2D(Collider2D collision) {

        if(!collision.gameObject.CompareTag("Player")) return;

        collision.gameObject.GetComponent<Player>().RemoveInteractable(gameObject);

    }

}
