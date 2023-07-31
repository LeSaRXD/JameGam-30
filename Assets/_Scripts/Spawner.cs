using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public Transform enemyTarget;
    public Transform enemies;

    public float timeBetweenWaves = 5f;

    private int waveNumber = 1;

    void Start() {

        StartCoroutine(Wave());

    }

    IEnumerator Wave() {

        while(true) {

            // todo UI
            Debug.Log("Wave " + waveNumber);

            int enemyCount = GetEnemyCount(waveNumber);
            for(int i = 0; i < enemyCount; i++) Spawn();

            yield return new WaitUntil(() => enemies.childCount == 0);
            
            // todo UI
            Debug.Log("Wave " + waveNumber + " complete!");

            yield return new WaitForSeconds(timeBetweenWaves);

            waveNumber++;

		}

	}

    int GetEnemyCount(int wave) {

        return 4;

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
