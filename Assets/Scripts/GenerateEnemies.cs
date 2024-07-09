using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class GenerateEnemies : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Enemy;
    private GameObject EnemyParent;
    public GameObject Player;

    private float distance = 200;
    private float yOffSet = 0;
    private int count;

    private const int defaultAddAlienAmount = 25;    
    private int INITIAL_ALIEN_COUNT = 50;
    private int alienCount;

    void Start()
    {
        //Get closest terrain to player 
        // Go through children (which should be terrain)
        // and get ranges for X and Z
        EnemyParent = new GameObject("EnemyParent");
        StartCoroutine(SpawnEnemies());
        alienCount = INITIAL_ALIEN_COUNT;
    }

    // Update is called once per frame
    void Update()
    {
        //Despawn if too far from player
    }

    public void addMoreAliens(int? additionalAliens = defaultAddAlienAmount) {
        print("Updating aliens from " + alienCount + " to " + (alienCount + additionalAliens));
        alienCount += (int)additionalAliens;
    }

    IEnumerator SpawnEnemies(){
        // while (this.count < alienCount) {
        while (this.count < alienCount) {
            print("Spawning " + this.count + "/" + alienCount);
            //Get closest terrain to player
            Terrain closestTerrain = getClosestTerrain();
            float yPos = closestTerrain.SampleHeight(new Vector3(0,0,0));
            Vector3 randomPos = this.Player.transform.position + (UnityEngine.Random.insideUnitSphere * distance);
            GameObject obj = Instantiate(Enemy, new Vector3(randomPos.x, yPos + yOffSet, randomPos.z), Quaternion.identity, EnemyParent.transform);
            EnemyAI script = obj.GetComponent<EnemyAI>();
            script.Player = this.Player;
            yield return new WaitForSeconds(1f);
            this.count++;
        }


    }

    Terrain getClosestTerrain(){
        Terrain[] terrains = Terrain.activeTerrains;
        Terrain terrain = null;
        Vector3 curPlayerPos = this.Player.transform.position;
        float closestDist = Mathf.Infinity;
        for(int i = 0; i < terrains.Length; i++){
            Terrain curTerain = terrains[i];
            Vector3 curTerrainPos = curTerain.GetPosition();
            float curDist = (curTerrainPos - curPlayerPos).sqrMagnitude;
            if(curDist < closestDist){
                terrain = curTerain;
                closestDist = curDist;
            }
        }
        return terrain;
    }
}
