using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

public class TimeManager : MonoBehaviour {
    
    public static TimeManager instance;

    public Spawner spawner;

    [Header("Post Processing")]
    public Volume postProcessVolume;
    public float weightChangeSpeed = 0.2f;
    
    float targetPostProcessingWeight = 0f;
    
    [Header("UI")]
    public GameObject deathPanel;
    public TextMeshProUGUI resultText;

	[Header("Audio")]
    public AudioSource bgmAudioSource;
    public AudioSource slowDownAudioSource;
    public AudioSource speedUpAudioSource;
    public AudioSource deathAudioSource;
    public float pitchUpdateSpeed = 0.25f;

    float currentPitch = 1f;
    float targetPitch = 1f;

    
    void Start() {
        
        if(instance == null) instance = this;

        bgmAudioSource = GetComponent<AudioSource>();

    }

    void Update() {
        
        UpdatePitch();
        UpdatePostProcessing();

    }

    public void Pause() {

        if(GameSettings.dead) return;

        GameSettings.paused = true;
        bgmAudioSource.Pause();
        slowDownAudioSource.Pause();
        speedUpAudioSource.Pause();

	}

    public void Resume() {

        GameSettings.paused = false;
        bgmAudioSource.Play();
        slowDownAudioSource.Play();

	}
    
	public void Stop() {

        GameSettings.dead = true;

        UpdateTimeScale(0f);
        deathPanel.SetActive(true);

        resultText.text = "You survived " + (spawner.waveNumber - 1).ToString() + " waves!";

    }

    public void UpdateTimeScale(float newTimeScale) {
        
        Time.timeScale = newTimeScale;
        if(newTimeScale > 0) Time.fixedDeltaTime = 0.02f * newTimeScale;

        targetPostProcessingWeight = 1f - newTimeScale;

        if(newTimeScale <= 0) {
            
            bgmAudioSource.Stop();
            deathAudioSource.Play();

		}
        if(targetPitch > newTimeScale) {
            
            speedUpAudioSource.Stop();
            slowDownAudioSource.Play();

        }
        else if(targetPitch < newTimeScale) {
            
            speedUpAudioSource.Play();
            slowDownAudioSource.Stop();

        }

        targetPitch = newTimeScale;

    }

    void UpdatePitch() {

        if(currentPitch == targetPitch) return;

        float delta = targetPitch - currentPitch;
        currentPitch += Mathf.Sign(delta) * Mathf.Min(Time.deltaTime * pitchUpdateSpeed, Mathf.Abs(delta));
        bgmAudioSource.pitch = currentPitch;

	}
    
    void UpdatePostProcessing() {
        
        float diff = targetPostProcessingWeight - postProcessVolume.weight;
        if(diff == 0) return;
        float change = Mathf.Sign(diff) * Mathf.Min(Mathf.Abs(diff), weightChangeSpeed * Time.unscaledDeltaTime);
        postProcessVolume.weight += change;

	}

}
