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
    public AudioSource effectsAudioSource;
	public AudioClip slowDownClip;
    public AudioClip speedUpClip;
	public AudioClip deathClip;
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
    
	public void Stop() {

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
            effectsAudioSource.PlayOneShot(deathClip, 1f);

		}
        if(targetPitch > newTimeScale) effectsAudioSource.PlayOneShot(slowDownClip, 1f);
        else if(targetPitch < newTimeScale) effectsAudioSource.PlayOneShot(speedUpClip, 1f);

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
