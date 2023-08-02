using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour {

	public TextMeshProUGUI highestWave;
    public GameObject settingsPanel;
    public Slider volume;
    public GameObject tutorialPanel;

    private void Start() {

        highestWave.text = "Highest wave: " + PlayerPrefs.GetInt("HighestWave", 0).ToString();
        volume.value = GameSettings.MasterVolume;

    }

    public void Play() {

		SceneManager.LoadScene("Game");

	}

    public void Settings() {

        if(settingsPanel.activeSelf) settingsPanel.SetActive(false);
        else settingsPanel.SetActive(true);

    }
    public void Tutorial() {

        if (tutorialPanel.activeSelf) tutorialPanel.SetActive(false);
        else tutorialPanel.SetActive(true);

    }

    public void OnVolumeChange() {
        GameSettings.MasterVolume = volume.value;
    }

}