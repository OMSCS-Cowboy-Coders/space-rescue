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

    private float distanceMin = 100;
    private float distanceMax = 500;
    private float spawnDistance = 50f;
    private float yOffSet = 0;
    private int count;
    private float minScale = 0.2f;
    private float maxScale = 1.0f;
    private const int defaultAddAlienAmount = 25;
    public int initialAlienCount = 10;
    public int maxAlienCount;
    private Coroutine spawnEnemyCoroutine;

    void Start()
    {
        //Get closest terrain to player 
        // Go through children (which should be terrain)
        // and get ranges for X and Z
        maxAlienCount = initialAlienCount;
        EnemyParent = new GameObject("EnemyParent");
        spawnEnemyCoroutine = StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        //Increase the number of aliens every second lmao
        maxAlienCount = initialAlienCount + (int) Time.realtimeSinceStartup;
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
        RaycastHit rayHit;
        TerrainGenerator terrainScript = GetComponent<TerrainGenerator>();
        while (this.count < maxAlienCount) {
            print("Spawning " + this.count + "/" + maxAlienCount);
            
            //Get closest terrain to player
            Terrain closestTerrain = terrainScript.getClosestTerrain(this.Player);
            //Choose random direction to spawn away from player
            float angle = UnityEngine.Random.Range(-Mathf.PI,Mathf.PI);
            Vector3 randomPosMin = this.Player.transform.position + new Vector3(Mathf.Cos(angle),0,Mathf.Sin(angle)) * distanceMin;
            Vector3 randomPosMax = this.Player.transform.position + new Vector3(Mathf.Cos(angle),0,Mathf.Sin(angle)) * distanceMax;
            Vector3 randomPos = Vector3.Lerp(randomPosMin, randomPosMax, UnityEngine.Random.value);
            //Convert randomPos coords to normalized terrain cords, assuming it is in the terrain
            Vector3 randomPosNormalized = terrainScript.WorldToTerrain(closestTerrain, randomPos);
            //Clamp the normalized randomPos within terrain bounds
            Vector3 randomPosClamped = clampPositionWithinTerrain(closestTerrain, randomPosNormalized);
            //Convert normalized clamped position to world position
            Vector3 randomPosWorld = terrainScript.TerrainToWorld(closestTerrain, randomPosClamped);
            //Raycast downwards, get the spot that is hit
            Physics.Raycast(randomPosWorld, Vector3.down,  out rayHit);
            randomPosWorld = rayHit.point;
            // Generate Enemy
            GameObject customEnemy = Instantiate(Enemy, randomPosWorld, Quaternion.identity, EnemyParent.transform);
            // Random enemy materials
            this.setRandomMeshColor(customEnemy);
            // Random enemy scale
            float scale = UnityEngine.Random.Range(minScale,maxScale);
            customEnemy.transform.localScale = new Vector3(scale,scale,scale);
            // Attach scripts to enemy
            EnemyAI AI_Script = customEnemy.AddComponent<EnemyAI>();
            AI_Script.Player = this.Player;
            AlienMotionController Motion_Script = customEnemy.AddComponent<AlienMotionController>();
            Motion_Script.Player = this.Player;
            yield return new WaitForSeconds(1f);
            this.count++;
        }
        print("Done with SpawnEnemy coroutine");

    }
    Vector3 clampPositionWithinTerrain(Terrain terrain, Vector3 normaliedCords){
        Vector3 clampedVector = new Vector3();
        TerrainGenerator terrainScript = terrain.GetComponent<TerrainGenerator>();
        //Grab bounds
        float clampWidthOffset = 100; //Spacing just in case its a tad too close to the boundary
        float clampLengthOffset = 100;
        float terrainWidth = terrainScript.terrainWidth;
        float terrainLength = terrainScript.terrainLength;
        float mountainWidthOffset = terrainScript.mountainWidthOffset;
        float mountainLengthOffset = terrainScript.mountainLengthOffset;
        //Bounds
        float xMin = (mountainWidthOffset + clampWidthOffset) / terrain.terrainData.size.x;
        float xMax = (terrainWidth - mountainWidthOffset - clampWidthOffset ) / terrain.terrainData.size.x;
        float zMin = (mountainLengthOffset + clampLengthOffset) / terrain.terrainData.size.z;
        float zMax = (terrainLength - mountainLengthOffset - clampLengthOffset) / terrain.terrainData.size.z;
        //Clamp the passed normalizedCords
        
        clampedVector.x = Mathf.Clamp(normaliedCords.x, xMin, xMax);
        clampedVector.y = normaliedCords.y;
        clampedVector.z = Mathf.Clamp(normaliedCords.z, zMin, zMax);
        return clampedVector;
    }

    void setRandomMeshColor(GameObject Enemy){
        //Generate random body, hair, and eyes.
        Material randomBody = EnemyBodies[UnityEngine.Random.Range(0,EnemyBodies.Length)];
        Material randomEyes = EnemyEyes[UnityEngine.Random.Range(0,EnemyEyes.Length)];
        Material randomHair = EnemyHairstyles[UnityEngine.Random.Range(0,EnemyHairstyles.Length)];
        
        Transform[] children = Enemy.GetComponentsInChildren<Transform>();
        foreach (Transform child in children){
            GameObject childObject = child.gameObject;
            Renderer renderer = childObject.GetComponent<Renderer>();
            if(child.name == "Head" || child.name == "Body" || child.name == "LeftEar" || child.name == "RightEar"){
                renderer.material = randomBody;
            }
            else if(child.name == "Eyes"){
                renderer.material = randomEyes;
            }
            else if(child.name == "Hair"){
                renderer.material = randomHair;
            }
        }
    }
}
