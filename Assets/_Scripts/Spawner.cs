using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float timeToSpawn = 0f;

    void Start() {
        


    }

    void Update() {
        
        timeToSpawn -= Time.deltaTime;
        if(timeToSpawn < 0f) Spawn();

    }

    void Spawn() {

        
        timeToSpawn = GetCooldown();

	}

    float GetCooldown() {

        // todo cooldown function
        return 0;

	}

}
