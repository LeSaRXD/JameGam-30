using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour {

    public readonly int maxHealth = 20;
    public int health {
        get {
            return health;
        }
        set {
            // todo change timescale
            health = value;
		}
    }

    void Start() {
        
        health = maxHealth;

    }

    void Update() {
        


    }

}
