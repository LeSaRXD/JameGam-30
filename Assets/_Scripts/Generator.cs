using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Generator : MonoBehaviour {

    public int maxHealth = 20;
    int health;
    public int Health {
        get {
            return health;
        }
        set {

            health = value;
            if(health <= 0) TimeManager.instance.Stop();
            else if(health <= 2) UpdateTimeScale(0.25f, darkRed);
            else if(health <= 5) UpdateTimeScale(0.5f, red);
            else if(health <= 10) UpdateTimeScale(0.75f, yellow);
            else if(health <= maxHealth) UpdateTimeScale(1f, green);
            else health = maxHealth;

            healthBar.value = health;

        }
    }

    [Header("UI")]
    public Slider healthBar;
    public Image healthBarImage;

    [Header("Colors")]
    public Color green;
    public Color yellow;
    public Color red;
    public Color darkRed;



    void Start() {
        
        UpdateTimeScale(1f, green);

        health = maxHealth;
        healthBar.maxValue = maxHealth;

    }

    void UpdateTimeScale(float newTimeScale, Color newColor) {

        TimeManager.instance.UpdateTimeScale(newTimeScale);
        healthBarImage.color = newColor;

    }

    void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Player>().interactables.Add(gameObject);
        else if(collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<Enemy>().generator = this;

    }

    private void OnTriggerExit2D(Collider2D collision) {

        if(collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Player>().interactables.Remove(gameObject);
        else if(collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<Enemy>().generator = null;

    }

}
