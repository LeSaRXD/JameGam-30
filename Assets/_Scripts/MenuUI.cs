using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour {

	public TextMeshProUGUI highestWave;

    private void Start() {

        highestWave.text = "Highest wave: " + PlayerPrefs.GetInt("HighestWave", 0).ToString();

    }

    public void Play() {

		SceneManager.LoadScene("Game");

	}

}