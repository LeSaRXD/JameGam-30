using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGenerator : MonoBehaviour {

    public GameObject[] treePrefabs;
    public int rows = 6, columns = 8;

    void Start() {

        float
            width = transform.localScale.x / columns,
            height = transform.localScale.y / rows;
        
        for(int row = 0; row < rows; row++) {
            for(int column = 0; column < columns; column++) {

                if((row == rows / 2 || row == (rows - 1) / 2) && (column == columns / 2 || column == (columns - 1) / 2)) continue;

                float
                    x = (column + Random.Range(0.1f, 0.9f)) * width - transform.localScale.x / 2f,
                    y = (row + Random.Range(0.1f, 0.9f)) * height - transform.localScale.y / 2f;

                GameObject tree = Instantiate(treePrefabs[Random.Range(0, treePrefabs.Length)]);
                tree.transform.position = new Vector2(x, y);

            }
        }

    }

}