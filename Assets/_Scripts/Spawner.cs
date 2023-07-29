using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject enemyPrefab;
    [SerializeField]
    private float timeToSpawn = 10f;

    void Update() {
        
        timeToSpawn -= Time.deltaTime;
        if(timeToSpawn < 0f) Spawn();

    }

    void Spawn() {

        GameObject enemy = Instantiate(enemyPrefab);

        enemy.transform.position = new Vector2(
            Random.Range(this.transform.position.x - this.transform.localScale.x / 2f, this.transform.position.x + this.transform.localScale.x / 2f),
            Random.Range(this.transform.position.y - this.transform.localScale.y / 2f, this.transform.position.y + this.transform.localScale.y / 2f)
        );
        
        timeToSpawn = GetCooldown();
        Debug.Log(timeToSpawn);

	}

    float GetCooldown() {

        float
            t = Time.time,
            a = 385f,
            o = 12.5f;

        return a / (t + o);

	}

}
