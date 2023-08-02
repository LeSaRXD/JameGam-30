using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [Header("Variables")]
    public float timeBetweenWaves = 5f;
    public float maxWaveDuration = 30f;

    [Header("References")]
    public GameObject enemyPrefab;
    public Transform enemyTarget;
    public Transform enemies;

    [Header("UI")]
    public TextMeshProUGUI waveCounter;
    public TextMeshProUGUI waveExecution;
    public Animation completeAnimation;

    public int waveNumber = 1;



    void Start() {

        StartCoroutine(Wave());

    }

    IEnumerator Wave() {

        while(true) {

            waveCounter.text = "Wave " + waveNumber;

            float waveStartTime = Time.time;

            int enemyCount = GetEnemyCount(waveNumber);
            for(int i = 0; i < enemyCount; i++) Spawn();

            yield return new WaitUntil(() => (
                (enemies.childCount == 0) ||
                (Time.time - waveStartTime > maxWaveDuration)
            ));

            waveExecution.text = "Wave " + waveNumber + " complete!";
            completeAnimation.Play();

            yield return new WaitForSeconds(timeBetweenWaves);

            waveNumber++;
            

		}

	}

    int GetEnemyCount(int wave) {

        return Mathf.Min((int)(1.89473684f * (wave - 1) + 4), 40);

	}

	void Spawn() {

        GameObject enemyObject = Instantiate(enemyPrefab, enemies);

        int spawnArea = Random.Range(0, transform.childCount);
        Transform area = transform.GetChild(spawnArea);

        enemyObject.transform.position = new Vector2(
            Random.Range(area.position.x - area.localScale.x / 2f, area.position.x + area.localScale.x / 2f),
            Random.Range(area.position.y - area.localScale.y / 2f, area.position.y + area.localScale.y / 2f)
        );
        
        enemyObject.GetComponent<Enemy>().target = enemyTarget;

	}

}
