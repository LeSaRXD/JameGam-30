using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour {

    public GameObject deathPanel;

    void Start() {

        if(deathPanel != null) deathPanel.SetActive(false);

    }

    public void Restart() {

        SceneManager.LoadScene("Game");
        deathPanel.SetActive(false);
    
    }

    public void MainMenu() {

        SceneManager.LoadScene("Menu");
        deathPanel.SetActive(false);

    }

    public void Play() {

        SceneManager.LoadScene("Game");

    }

}
