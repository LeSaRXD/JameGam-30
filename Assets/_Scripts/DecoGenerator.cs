using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecoGenerator : MonoBehaviour {

    public int count = 100;
    public Transform decorations;

    [System.Serializable]
    public struct PrefabWeight {
        public GameObject prefab;
        public float weight;
	}
    public PrefabWeight[] prefabsWeights;
    
    void Start() {

        float totalWeights = 0;
        foreach(PrefabWeight pw in prefabsWeights) totalWeights += pw.weight;

        for(int i = 0; i < count; i++) {

            float targetWeight = Random.Range(0, totalWeights);
            
            float totalWeight = prefabsWeights[0].weight;
            int index = 0;
            while(totalWeight < targetWeight) {
                
                totalWeight += prefabsWeights[index + 1].weight;
                index++;

            }
            GameObject decoration = Instantiate(prefabsWeights[index].prefab, decorations);
            decoration.transform.position = new(
                Random.Range(-transform.localScale.x, transform.localScale.x) / 2f + transform.position.x,
                Random.Range(-transform.localScale.y, transform.localScale.y) / 2f + transform.position.y
            );

            if(Random.value > 0.5f) decoration.GetComponent<SpriteRenderer>().flipX = true;

        }
        
    }
    
}
