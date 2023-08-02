using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
    
    public GameObject pausePanel;
    public GameObject deathPanel;
    public Slider volumeSlider;

    void Start() {

        deathPanel.SetActive(false);
        pausePanel.SetActive(false);
        volumeSlider.value = GameSettings.MasterVolume;

    }

	void Update() {
		
        if(Input.GetKeyDown(KeyCode.Escape)) Pause();

	}

    public void Pause() {

        if(GameSettings.paused) {

            pausePanel.SetActive(false);
            TimeManager.instance.Resume();

		} else {

            pausePanel.SetActive(true);
            TimeManager.instance.Pause();

        }
            
	}

    public void Restart() {
        
        GameSettings.Reset();
        SceneManager.LoadScene("Game");
        
    }

    public void MainMenu() {

        GameSettings.Reset();
        SceneManager.LoadScene("Menu");

    }
    
    public void OnVolumeChange() {

        GameSettings.MasterVolume = volumeSlider.value;

    }

}
