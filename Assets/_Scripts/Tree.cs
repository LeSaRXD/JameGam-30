using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public GameObject[] cogs;
    public readonly float growCooldown;
    public float timeToGrow;

    void Start() {
        
    }

    void Update() {
        
        timeToGrow -= Time.deltaTime;
        if(timeToGrow <= 0) Grow();

    }

    void Grow() {

        // todo change cogs
        timeToGrow = growCooldown;

	}

}
