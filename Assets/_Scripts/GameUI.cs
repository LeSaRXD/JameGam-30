using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {
    
    public TimeManager timeManager;

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
            timeManager.Resume();

		} else {

            pausePanel.SetActive(true);
            timeManager.Pause();

        }
            
	}

	public void Resume() {

        pausePanel.SetActive(false);
        timeManager.Resume();

	}

    public void Restart() {

        SceneManager.LoadScene("Game");
        
    }

    public void MainMenu() {

        SceneManager.LoadScene("Menu");

    }
    
    public void OnVolumeChange() {

        GameSettings.MasterVolume = volumeSlider.value;

    }

}
