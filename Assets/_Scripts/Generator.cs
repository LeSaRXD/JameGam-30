using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Generator : MonoBehaviour {

    public int maxHealth = 20;
    int health;
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

    [Header("Colors")]
    public Color green;
    public Color yellow;
    public Color red;
    public Color darkRed;

    public Slider healthBar;
    public Image healthBarImage;

    [Header("Post Processing")]
    public float weightChangeSpeed = 0.2f;
    private float targetWeight = 0f;
    public Volume postProcessVolume;

    public GameObject deathPanel; // UI

    void Start() {
        
        UpdateTimeScale(1f, green);

        health = maxHealth;
        healthBar.maxValue = maxHealth;

    }

	private void Update() {

        UpdatePostProcessing();
		
	}

	void Stop() {

        UpdateTimeScale(0f, green);
        deathPanel.SetActive(true);

    }

    void UpdateTimeScale(float newTimeScale, Color newColor) {

        Time.timeScale = newTimeScale;
        Time.fixedDeltaTime = 0.02f * newTimeScale;

        healthBarImage.color = newColor;

        targetWeight = 1f - newTimeScale;

    }

    void UpdatePostProcessing() {
        
        float diff = targetWeight - postProcessVolume.weight;
        if(diff == 0) return;
        float change = Mathf.Sign(diff) * Mathf.Min(Mathf.Abs(diff), weightChangeSpeed * Time.unscaledDeltaTime);
        postProcessVolume.weight += change;

	}

    void OnTriggerEnter2D(Collider2D collision) {

        if(collision.gameObject.CompareTag("Player"))
            collision.gameObject.GetComponent<Player>().interactables.Add(gameObject);
        else if(collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<Enemy>().generator = this;

    }

    private void OnTriggerExit2D(Collider2D collision) {

        if(!collision.gameObject.CompareTag("Player")) return;

        collision.gameObject.GetComponent<Player>().interactables.Remove(gameObject);

    }

}
