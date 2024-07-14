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

    public Material[] EnemyHairstyles;
    public Material [] EnemyEyes;
    public Material [] EnemyBodies;

    private GameObject EnemyParent;
    public GameObject Player;

    private float distance = 200;
    private float yOffSet = 0;
    private int count;
    private float minScale = 0.2f;
    private float maxScale = 1.0f;
    private const int defaultAddAlienAmount = 25;
    private int INITIAL_ALIEN_COUNT = 50;
    private int maxAlienCount;
    private Coroutine spawnEnemyCoroutine;

    void Start()
    {
        //Get closest terrain to player 
        // Go through children (which should be terrain)
        // and get ranges for X and Z
        EnemyParent = new GameObject("EnemyParent");
        maxAlienCount = INITIAL_ALIEN_COUNT;
        spawnEnemyCoroutine = StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        //Despawn if too far from player
    }

    public void addMoreAliens(int? additionalAliens = defaultAddAlienAmount) {
        print("Updating aliens from " + maxAlienCount + " to " + (maxAlienCount + additionalAliens));
        maxAlienCount += (int)additionalAliens;
        if (spawnEnemyCoroutine != null) {
            print("Restarting ongoing coroutine");
            StopCoroutine(spawnEnemyCoroutine);
        }
        spawnEnemyCoroutine = StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies(){
        // while (this.count < maxAlienCount) {
        while (this.count < maxAlienCount) {
            print("Spawning " + this.count + "/" + maxAlienCount);
            //Get closest terrain to player
            Terrain closestTerrain = getClosestTerrain();
            float yPos = closestTerrain.SampleHeight(new Vector3(0,0,0));
            Vector3 randomPos = this.Player.transform.position + (UnityEngine.Random.insideUnitSphere * distance);
            // Random enemy materials
            GameObject customEnemy = this.setRandomMeshColor(Enemy);
            // Random enemy scale
            float scale = UnityEngine.Random.Range(minScale,maxScale);
            customEnemy.transform.localScale = new Vector3(scale,scale,scale);
            // Attach script to enemy
            EnemyAI script = customEnemy.GetComponent<EnemyAI>();
            script.Player = this.Player;
            // Generate Enemy
            GameObject obj = Instantiate(customEnemy, new Vector3(randomPos.x, yPos + yOffSet, randomPos.z), Quaternion.identity, EnemyParent.transform);
            yield return new WaitForSeconds(1f);
            this.count++;
        }
        print("Done with SpawnEnemy coroutine");

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

    GameObject setRandomMeshColor(GameObject Enemy){
        //Generate random body, hair, and eyes.
        Material randomBody = EnemyBodies[UnityEngine.Random.Range(0,EnemyBodies.Length)];
        Material randomEyes = EnemyEyes[UnityEngine.Random.Range(0,EnemyEyes.Length)];
        Material randomHair = EnemyHairstyles[UnityEngine.Random.Range(0,EnemyHairstyles.Length)];
        
        GameObject mesh = Enemy.transform.Find("Mesh").gameObject;
        Transform[] meshChildren = mesh.GetComponentsInChildren<Transform>();
        foreach (Transform children in meshChildren){
            GameObject childObject = children.gameObject;
            Renderer renderer = childObject.GetComponent<Renderer>();
            if(children.name == "Head" || children.name == "Body" || children.name == "LeftEar" || children.name == "RightEar"){
                renderer.material = randomBody;
            }
            else if(children.name == "Eyes"){
                renderer.material = randomEyes;
            }
            else if(children.name == "Hair"){
                renderer.material = randomHair;
            }
        }

        return Enemy;
    }

}
