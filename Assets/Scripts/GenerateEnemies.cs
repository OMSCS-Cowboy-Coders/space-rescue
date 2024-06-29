using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;
    public GameObject EnemyParent;
    private float xMin = Mathf.Infinity;
    private float xMax = -Mathf.Infinity;
    private float zMin = Mathf.Infinity;
    private float zMax = -Mathf.Infinity;


    private int count;
    void Start()
    {
        // Go through children (which should be terrain)
        // and get ranges for X and Z
        for(int i = 0; i < this.gameObject.transform.childCount; i++){
            Transform child = this.gameObject.transform.GetChild(i);
            float childX = child.position.x;
            float childZ = child.position.z;
            this.xMin = Mathf.Min(xMin, childX);
            this.xMax = Mathf.Max(xMax, childX);
            this.zMin = Mathf.Min(zMin, childZ);
            this.zMax = Mathf.Max(zMax, childZ);
        }
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies(){
        while (this.count < 50){
            float randomX = Random.Range(xMin, xMax);
            float randomZ = Random.Range(zMin, zMax);
            Instantiate(Enemy, new Vector3(randomX, 0, randomZ), Quaternion.identity, EnemyParent.transform);
            yield return new WaitForSeconds(0.1f);
            this.count++;
        }


    }
}
