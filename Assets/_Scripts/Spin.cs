using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float degreesPerMinute;

    void Update() {
        
        transform.localEulerAngles += Vector3.back * degreesPerMinute / 60f * Time.deltaTime;

    }

}
