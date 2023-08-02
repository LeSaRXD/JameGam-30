using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour {

    public GameObject deathPanel;

    void Start() {

        deathPanel.SetActive(false);

    }

    public void Restart() {

        SceneManager.LoadScene("Game");
        
    }

    public void MainMenu() {

        SceneManager.LoadScene("Menu");

    }

}
